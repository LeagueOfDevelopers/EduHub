using System;
using System.IO;
using System.Text;
using EduHub.Extensions;
using EduHub.Filters;
using EduHub.Middleware;
using EduHub.Security;
using EduHubLibrary.Data;
using EduHubLibrary.Domain;
using EduHubLibrary.Domain.Consumers;
using EduHubLibrary.Domain.NotificationService;
using EduHubLibrary.EventBus.EventTypes;
using EduHubLibrary.Facades;
using EduHubLibrary.Infrastructure;
using EduHubLibrary.Mailing;
using EduHubLibrary.Settings;
using Loggly;
using Loggly.Config;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Serilog;
using Serilog.Events;
using Swashbuckle.AspNetCore.Examples;
using Swashbuckle.AspNetCore.Swagger;

namespace EduHub
{
    public class Startup
    {
        public Startup(IConfiguration configuration, IHostingEnvironment env)
        {
            Configuration = configuration;
            Env = env;
        }

        public IConfiguration Configuration { get; }
        public IHostingEnvironment Env { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            StartLoggly();

            services.AddWebSocketManager();

            IFileRepository fileRepository;
            IGroupRepository groupRepository;
            IKeysRepository keysRepository;
            ITagRepository tagRepository;
            IUserRepository userRepository;
            ISanctionRepository sanctionRepository;
            IEventRepository eventRepository;

            if (bool.Parse(Configuration.GetValue<string>("UseDB")))
            {
                var dbContext = Configuration.GetValue<string>("MysqlConnectionString");
                using (var context = new EduhubContext(dbContext))
                {
                    if (bool.Parse(Configuration.GetValue<string>("DeleteDB"))) context.Database.EnsureDeleted();
                    if (context.Database.EnsureCreated())
                    {
                        var dbName = dbContext.Split("database=")[1].Split(";")[0];
                        context.Database.ExecuteSqlCommand(
                            "ALTER DATABASE " + dbName + " CHARACTER SET utf8 COLLATE utf8_general_ci;");
                        var modelNames = context.Model.GetEntityTypes();
                        foreach (var modelname in modelNames)
                        {
                            var mapping = context.Model.FindEntityType(modelname.Name).Relational();
                            var tableName = mapping.TableName;
                            context.Database.ExecuteSqlCommand(
                                "alter table " + tableName.ToLower()
                                               + " convert to character set utf8 collate utf8_unicode_ci;");
                        }
                    }

                    context.Database.Migrate();
                }

                fileRepository = new InMysqlFileRepository(dbContext);
                groupRepository = new InMysqlGroupRepository(dbContext);
                keysRepository = new InMysqlKeyRepository(dbContext);
                tagRepository = new InMysqlTagRepository(dbContext);
                userRepository = new InMysqlUserRepository(dbContext);
                sanctionRepository = new InMysqlSanctionRepository(dbContext);
                eventRepository = new InMemoryEventRepository();
            }
            else
            {
                fileRepository = new InMemoryFileRepository();
                groupRepository = new InMemoryGroupRepository();
                keysRepository = new InMemoryKeysRepository();
                tagRepository = new InMemoryTagRepository();
                sanctionRepository = new InMemorySanctionRepository();
                userRepository = new InMemoryUserRepository();
                eventRepository = new InMemoryEventRepository();
            }

            var emailSettings = new EmailSettings(Configuration.GetValue<string>("EmailLogin"),
                Configuration.GetValue<string>("Email"),
                Configuration.GetValue<string>("EmailPassword"),
                Configuration.GetValue<string>("SmtpAddress"),
                Configuration.GetValue<string>("ConfirmAddress"),
                int.Parse(Configuration.GetValue<string>("SmtpPort")));


            var defaultAvatarFilename = Configuration.GetValue<string>("DefaultAvatarFilename");
            var defaultAvatarContentType = Configuration.GetValue<string>("DefaultAvatarContentType");
            var userSettings = new UserSettings(defaultAvatarFilename);
            if (!fileRepository.DoesFileExists(defaultAvatarFilename))
            { 
                fileRepository.AddFile(new UserFile(defaultAvatarFilename, defaultAvatarContentType));
            }

            var tagFacade = new TagFacade(tagRepository);
            var emailSender = new EmailSender(emailSettings);
            var notificationsDistributor = new NotificationsDistributor(groupRepository, userRepository, emailSender);

            var groupSettings = new GroupSettings(Configuration.GetValue<int>("MinGroupSize"),
                Configuration.GetValue<int>("MaxGroupSize"),
                Configuration.GetValue<double>("MinGroupValue"),
                Configuration.GetValue<double>("MaxGroupValue"));

            var eventBusSettings = new EventBusSettings(Configuration.GetValue<string>("RabbitMqServerHostName"),
                Configuration.GetValue<string>("RabbitMqServerVirtualHost"),
                Configuration.GetValue<string>("RabbitMqAdminUserName"),
                Configuration.GetValue<string>("RabbitMqAdminPassword"));
            var eventBus = new EventBus(eventBusSettings);
            eventBus.StartListening();

            var adminsEventConsumer = new AdminsEventConsumer(notificationsDistributor, eventRepository);
            var courseEventConsumer = new CourseEventConsumer(notificationsDistributor, eventRepository);
            var curriculumEventConsumer = new CurriculumEventConsumer(notificationsDistributor, eventRepository);
            var groupEventsConsumer = new GroupEventsConsumer(notificationsDistributor, eventRepository);
            var invitationConsumer = new InvitationConsumer(notificationsDistributor, eventRepository);
            var memberActionsConsumer = new MemberActionsConsumer(notificationsDistributor, eventRepository);

            eventBus.RegisterConsumer(new TagPopularityConsumer(tagFacade));
            eventBus.RegisterConsumer<ReportMessageEvent>(adminsEventConsumer);
            eventBus.RegisterConsumer<SanctionsAppliedEvent>(adminsEventConsumer);
            eventBus.RegisterConsumer<SanctionCancelledEvent>(adminsEventConsumer);
            eventBus.RegisterConsumer<TeacherFoundEvent>(courseEventConsumer);
            eventBus.RegisterConsumer<CourseFinishedEvent>(courseEventConsumer);
            eventBus.RegisterConsumer<ReviewReceivedEvent>(courseEventConsumer);
            eventBus.RegisterConsumer<CurriculumAcceptedEvent>(curriculumEventConsumer);
            eventBus.RegisterConsumer<CurriculumDeclinedEvent>(curriculumEventConsumer);
            eventBus.RegisterConsumer<CurriculumSuggestedEvent>(curriculumEventConsumer);
            eventBus.RegisterConsumer<NewCreatorEvent>(groupEventsConsumer);
            eventBus.RegisterConsumer<GroupIsFormedEvent>(groupEventsConsumer);
            eventBus.RegisterConsumer<InvitationAcceptedEvent>(invitationConsumer);
            eventBus.RegisterConsumer<InvitationDeclinedEvent>(invitationConsumer);
            eventBus.RegisterConsumer<InvitationReceivedEvent>(invitationConsumer);
            eventBus.RegisterConsumer<NewMemberEvent>(memberActionsConsumer);
            eventBus.RegisterConsumer<MemberLeftEvent>(memberActionsConsumer);

            var publisher = eventBus.GetEventPublisher();

            var userFacade = new UserFacade(userRepository, groupRepository, eventRepository, publisher);
            var groupEditFacade = new GroupEditFacade(groupRepository, groupSettings, publisher);
            var userEditFacade = new UserEditFacade(userRepository, fileRepository, sanctionRepository);
            var groupFacade = new GroupFacade(groupRepository, userRepository, sanctionRepository, groupSettings,
                publisher);
            var fileFacade = new FileFacade(fileRepository);
            var chatFacade = new ChatFacade(groupRepository, userRepository);
            var sanctionsFacade = new SanctionFacade(sanctionRepository, userRepository, publisher);
            var userAccountFacade = new AccountFacade(keysRepository, userRepository, emailSender, userSettings);
            var reportFacade = new ReportFacade(userRepository, eventRepository, publisher);
            services.AddSingleton<IUserFacade>(userFacade);
            services.AddSingleton<IGroupFacade>(groupFacade);
            services.AddSingleton<IFileFacade>(fileFacade);
            services.AddSingleton<IChatFacade>(chatFacade);
            services.AddSingleton<IGroupEditFacade>(groupEditFacade);
            services.AddSingleton<IUserEditFacade>(userEditFacade);
            services.AddSingleton<ITagFacade>(tagFacade);
            services.AddSingleton<ISanctionFacade>(sanctionsFacade);
            services.AddSingleton<IAccountFacade>(userAccountFacade);
            services.AddSingleton<IReportFacade>(reportFacade);
            services.AddSingleton(Env);

            userAccountFacade.CheckAdminExistence(Configuration.GetValue<string>("AdminEmail"));

            services.AddSwaggerGen(current =>
            {
                current.SwaggerDoc("v1", new Info
                {
                    Title = "EduHub API",
                    Version = "v1"
                });
                current.AddSecurityDefinition("Bearer", new ApiKeyScheme
                {
                    Description =
                        "JWT Authorization header using the Bearer scheme. Example: \"Authorization: Bearer {token}\"",
                    Name = "Authorization",
                    In = "header",
                    Type = "apiKey"
                });
                current.OperationFilter<ExamplesOperationFilter>();
                current.DescribeAllEnumsAsStrings();
                var a = string.Format(@"{0}\EduHub.xml", AppDomain.CurrentDomain.BaseDirectory);
                current.IncludeXmlComments(string.Format(@"{0}/EduHub.xml", AppDomain.CurrentDomain.BaseDirectory));
            });
            ConfigureSecurity(services);
            if (Configuration.GetValue<bool>("Authorization"))
                services.AddMvc(o =>
                {
                    o.Filters.Add(new ExceptionFilter());
                    o.Filters.Add(new ActionFilter());
                });
            else
                services.AddMvc(o =>
                {
                    o.Filters.Add(new AllowAnonymousFilter());
                    o.Filters.Add(new ExceptionFilter());
                    o.Filters.Add(new ActionFilter());
                });
            services.AddCors(options =>
            {
                options.AddPolicy("AllowAnyOrigin",
                    builder => builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env,
            IServiceProvider serviceProvider)
        {
            app.UseSwagger();
            app.UseStaticFiles();
            app.UseWebSockets();
            app.MapWebSocketManager("/api/sockets/creation",
                serviceProvider.GetService<NotificationsMessageHandler>());

            app.UseSwaggerUI(current => { current.SwaggerEndpoint("/swagger/v1/swagger.json", "EduHub API"); });
            if (env.IsDevelopment()) app.UseDeveloperExceptionPage();
            app.UseCors("AllowAnyOrigin");

            app.UseMvc();
        }

        private void StartLoggly()
        {
            var config = LogglyConfig.Instance;
            config.CustomerToken = Configuration.GetValue<string>("LogglyToken");
            config.ApplicationName = "EduHubWebApi";

            config.Transport.EndpointHostname = "logs-01.loggly.com";
            config.Transport.EndpointPort = 443;
            config.Transport.LogTransport = LogTransport.Https;

            var ct = new ApplicationNameTag();
            ct.Formatter = "application-{0}";
            config.TagConfig.Tags.Add(ct);

            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Information()
                .MinimumLevel.Override("Microsoft", LogEventLevel.Information)
                .Enrich.FromLogContext()
                .WriteTo.Loggly()
                .CreateLogger();
            Log.Information("Loggly started");
        }


        private void ConfigureSecurity(IServiceCollection services)
        {
            var securityConfiguration = Configuration.GetSection("Security");
            var securitySettings = new SecuritySettings(
                securityConfiguration["EncryptionKey"], securityConfiguration["Issue"],
                securityConfiguration.GetValue<TimeSpan>("ExpirationPeriod"));
            var jwtIssuer = new JwtIssuer(securitySettings);
            services.AddSingleton(securitySettings);
            services.AddSingleton<IJwtIssuer>(jwtIssuer);

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = false,
                        ValidateAudience = false,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ClockSkew = TimeSpan.Zero,
                        IssuerSigningKey = new SymmetricSecurityKey(
                            Encoding.UTF8.GetBytes(securitySettings.EncryptionKey))
                    };
                });

            services
                .AddAuthorization(options =>
                {
                    options.DefaultPolicy =
                        new AuthorizationPolicyBuilder(JwtBearerDefaults.AuthenticationScheme)
                            .RequireAuthenticatedUser().Build();

                    options.AddPolicy("AdminOnly",
                        new AuthorizationPolicyBuilder(JwtBearerDefaults.AuthenticationScheme)
                            .RequireClaim(Claims.Roles.RoleClaim, Claims.Roles.Admin).Build());

                    options.AddPolicy("ModeratorsOnly",
                        new AuthorizationPolicyBuilder(JwtBearerDefaults.AuthenticationScheme)
                            .RequireClaim(Claims.Roles.RoleClaim, Claims.Roles.Moderator).Build());

                    options.AddPolicy("AdminAndModeratorsOnly",
                        new AuthorizationPolicyBuilder(JwtBearerDefaults.AuthenticationScheme)
                            .RequireClaim(Claims.Roles.RoleClaim, Claims.Roles.Admin, Claims.Roles.Moderator).Build());
                });
        }
    }
}