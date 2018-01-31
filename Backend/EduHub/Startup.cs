﻿using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Swashbuckle.AspNetCore.Swagger;
using EduHubLibrary.Facades;
using EduHubLibrary.Infrastructure;
using EduHubLibrary.Settings;
using EduHub.Filters;
using EduHub.Security;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.AspNetCore.Authorization;
using Loggly.Config;
using Loggly;
using Serilog;
using Microsoft.AspNetCore.Mvc.Authorization;
using Swashbuckle.AspNetCore.Examples;
using System;
using Serilog.Events;
using EduHubLibrary.Domain.NotificationService;

namespace EduHub
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            StartLoggly();
            var groupSettings = new GroupSettings(Configuration.GetValue<int>("MinGroupSize"),
                Configuration.GetValue<int>("MaxGroupSize"),
                Configuration.GetValue<double>("MinGroupValue"),
                Configuration.GetValue<double>("MaxGroupValue"));
            var userRepository = new InMemoryUserRepository();
            var groupRepository = new InMemoryGroupRepository();
            var messageBus = new MessageBus();
            var userFacade = new UserFacade(userRepository, groupRepository);
            var groupFacade = new GroupFacade(groupRepository, userRepository, groupSettings, messageBus);
            services.AddSingleton<IUserFacade>(userFacade);
            services.AddSingleton<IGroupFacade>(groupFacade);
            services.AddSwaggerGen(current =>
            {
                current.SwaggerDoc("v1", new Info
                {
                    Title = "EduHub API",
                    Version = "v1"
                });
                current.AddSecurityDefinition("Bearer", new ApiKeyScheme
                {
                    Description = "JWT Authorization header using the Bearer scheme. Example: \"Authorization: Bearer {token}\"",
                    Name = "Authorization",
                    In = "header",
                    Type = "apiKey"
                });
                current.OperationFilter<ExamplesOperationFilter>();
                current.DescribeAllEnumsAsStrings();
                string a = string.Format(@"{0}\EduHub.xml", AppDomain.CurrentDomain.BaseDirectory);
                current.IncludeXmlComments(string.Format(@"{0}/EduHub.xml", AppDomain.CurrentDomain.BaseDirectory));
            });
            ConfigureSecurity(services);
            if (Configuration.GetValue<bool>("Authorization"))
            {
                services.AddMvc(o => {
                    o.Filters.Add(new ExceptionFilter());
                    o.Filters.Add(new ActionFilter());
                });
            }
            else
            {
                services.AddMvc(o => {
                    o.Filters.Add(new AllowAnonymousFilter());
                    o.Filters.Add(new ExceptionFilter());
                    o.Filters.Add(new ActionFilter());
                });
            }
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

            string path = env.ContentRootPath;

            app.UseSwaggerUI(current =>
            {
                current.SwaggerEndpoint("/swagger/v1/swagger.json", "EduHub API");
            });
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
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
                securityConfiguration["EncryptionKey"], securityConfiguration["Issue"], securityConfiguration.GetValue<System.TimeSpan>("ExpirationPeriod"));
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

                    options.AddPolicy("GeneralAdminOnly", new AuthorizationPolicyBuilder(JwtBearerDefaults.AuthenticationScheme)
                    .RequireClaim(Claims.Roles.RoleClaim, Claims.Roles.GeneralAdmin).Build());

                    options.AddPolicy("AdminsOnly", new AuthorizationPolicyBuilder(JwtBearerDefaults.AuthenticationScheme)
                    .RequireClaim(Claims.Roles.RoleClaim, Claims.Roles.Admin, Claims.Roles.GeneralAdmin).Build());
                });
        }
    } 
}
