using System;
using System.Text;
using EduHub.Filters;
using EduHub.Security;
using EduHubLibrary.Data;
using EduHubLibrary.Domain;
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
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Serilog;
using Serilog.Events;
using Swashbuckle.AspNetCore.Examples;
using Swashbuckle.AspNetCore.Swagger;
using EduHubLibrary.Domain.NotificationService;
using EduHubLibrary.Domain.Consumers;
using Microsoft.EntityFrameworkCore;
using EduHubLibrary.Domain.Events;

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

            IFileRepository fileRepository;
            IGroupRepository groupRepository;
            IKeysRepository keysRepository;
            ITagRepository tagRepository;
            IUserRepository userRepository;
            ISanctionRepository sanctionRepository;

            if (bool.Parse(Configuration.GetValue<string>("UseDB")))
            {
                var dbContext = Configuration.GetValue<string>("MysqlConnectionString");
                using (var context = new EduhubContext(dbContext))
                {
                    if (bool.Parse(Configuration.GetValue<string>("DeleteDB")))
                    {
                        context.Database.EnsureDeleted();
                    }
                    context.Database.EnsureCreated();
                    context.Database.Migrate();
                }
                fileRepository = new InMysqlFileRepository(dbContext);
                groupRepository = new InMysqlGroupRepository(dbContext);
                keysRepository = new InMysqlKeyRepository(dbContext);
                tagRepository = new InMysqlTagRepository(dbContext);
                userRepository = new InMysqlUserRepository(dbContext);
                sanctionRepository = new InMysqlSanctionRepository(dbContext);
            }
            else
            {
                fileRepository = new InMemoryFileRepository();
                groupRepository = new InMemoryGroupRepository();
                keysRepository = new InMemoryKeysRepository();
                tagRepository = new InMemoryTagRepository();
                sanctionRepository = new InMemorySanctionRepository();
                userRepository = new InMemoryUserRepository();
            }

            var emailSettings = new EmailSettings(Configuration.GetValue<string>("EmailLogin"),
                Configuration.GetValue<string>("Email"),
                Configuration.GetValue<string>("EmailPassword"),
                Configuration.GetValue<string>("SmtpAddress"),
                Configuration.GetValue<string>("ConfirmAddress"),
                int.Parse(Configuration.GetValue<string>("SmtpPort")));

            var tagFacade = new TagFacade(tagRepository);
            var notificationsDistributor = new NotificationsDistributor(groupRepository, userRepository);

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

            eventBus.RegisterConsumer(new TagPopularityConsumer(tagFacade));
            eventBus.RegisterConsumer<ReportMessageEvent>(new AdminsEventConsumer(notificationsDistributor));
            eventBus.RegisterConsumer<SanctionsAppliedEvent>(new AdminsEventConsumer(notificationsDistributor));
            eventBus.RegisterConsumer<TeacherFoundEvent>(new CourseEventConsumer(notificationsDistributor));
            eventBus.RegisterConsumer<CourseFinishedEvent>(new CourseEventConsumer(notificationsDistributor));
            eventBus.RegisterConsumer<ReviewReceivedEvent>(new CourseEventConsumer(notificationsDistributor));
            eventBus.RegisterConsumer<CurriculumAcceptedEvent>(new CurriculumEventConsumer(notificationsDistributor));
            eventBus.RegisterConsumer<CurriculumDeclinedEvent>(new CurriculumEventConsumer(notificationsDistributor));
            eventBus.RegisterConsumer<CurriculumSuggestedEvent>(new CurriculumEventConsumer(notificationsDistributor));
            eventBus.RegisterConsumer<NewCreatorEvent>(new GroupEventsConsumer(notificationsDistributor));
            eventBus.RegisterConsumer<GroupIsFormedEvent>(new GroupEventsConsumer(notificationsDistributor));
            eventBus.RegisterConsumer<InvitationAcceptedEvent>(new InvitationConsumer(notificationsDistributor));
            eventBus.RegisterConsumer<InvitationDeclinedEvent>(new InvitationConsumer(notificationsDistributor));
            eventBus.RegisterConsumer<InvitationReceivedEvent>(new InvitationConsumer(notificationsDistributor));
            eventBus.RegisterConsumer<NewMemberEvent>(new MemberActionsConsumer(notificationsDistributor));
            eventBus.RegisterConsumer<MemberLeftEvent>(new MemberActionsConsumer(notificationsDistributor));

            var publisher = eventBus.GetEventPublisher();

            var emailSender = new EmailSender(emailSettings);
            var userFacade = new UserFacade(userRepository, groupRepository, keysRepository, publisher);
            var groupEditFacade = new GroupEditFacade(groupRepository, groupSettings, publisher);
            var userEditFacade = new UserEditFacade(userRepository, fileRepository, sanctionRepository);
            var groupFacade = new GroupFacade(groupRepository, userRepository, sanctionRepository, groupSettings, publisher);
            var fileFacade = new FileFacade(fileRepository);
            var chatFacade = new ChatFacade(groupRepository);
            var sanctionsFacade = new SanctionFacade(sanctionRepository, userRepository, publisher);
            var userAccountFacade = new AccountFacade(keysRepository, userRepository, emailSender);
            services.AddSingleton<IUserFacade>(userFacade);
            services.AddSingleton<IGroupFacade>(groupFacade);
            services.AddSingleton<IFileFacade>(fileFacade);
            services.AddSingleton<IChatFacade>(chatFacade);
            services.AddSingleton<IGroupEditFacade>(groupEditFacade);
            services.AddSingleton<IUserEditFacade>(userEditFacade);
            services.AddSingleton<ITagFacade>(tagFacade);
            services.AddSingleton<ISanctionFacade>(sanctionsFacade);
            services.AddSingleton<IAccountFacade>(userAccountFacade);
            services.AddSingleton(Env);

            //userAccountFacade.CheckAdminExistence(Configuration.GetValue<string>("AdminEmail"));

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
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseSwagger();
            app.UseStaticFiles();

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