using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.Swagger;
using EduHubLibrary.Facades;
using EduHubLibrary.Infrastructure;


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
            var userRepository = new InMemoryUserRepository();
            var userFacade = new UserFacade(userRepository);
            var groupRepository = new InMemoryGroupRepository();
            var groupFacade = new GroupFacade(groupRepository, userRepository);
            services.AddSingleton<IUserFacade>(userFacade);
            services.AddSingleton<IGroupFacade>(groupFacade);
            services.AddMvc();
            services.AddSwaggerGen(current => {
                current.SwaggerDoc("v1", new Info{
                    Title = "EduHub API",
                    Version = "v1"
                });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseSwagger();

            app.UseSwaggerUI(current => { 
                current.SwaggerEndpoint("/swagger/v1/swagger.json", "EduHub API");
            });

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMvc();
        }
    }
}
