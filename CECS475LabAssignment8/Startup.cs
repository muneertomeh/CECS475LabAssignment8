using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CECS475LabAssignment8.DAL;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Protocols;


namespace CECS475LabAssignment8
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called sssby the runtime. Uske this method to add services to the dcontainer.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            


            services.AddDbContext<AcademicContext>(options =>
            {
                var connectionString = Configuration.GetConnectionString("AcademicContext");
                options.UseSqlServer(connectionString);
            });

            services.AddDbContext<IdentityDataContext>(options =>
            {
                var connectionString = Configuration.GetConnectionString("IdentityDataContext");
                options.UseSqlServer(connectionString);
            });


            services.AddIdentity<IdentityUser, IdentityRole>()
                .AddEntityFrameworkStores<IdentityDataContext>() ;


            services.AddMvc();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseExceptionHandler("/error.html");


            var configuration = new ConfigurationBuilder().AddEnvironmentVariables().
                                                            AddJsonFile(env.ContentRootPath + "/config.json").
                                                            AddJsonFile(env.ContentRootPath + "/config.development.json", true).Build();
            
            if (configuration.GetValue<bool>("FeatureToggles:EnableDeveloperExceptions"))
            {
                app.UseDeveloperExceptionPage();
            }
            

            app.Use(async (context, next) =>
            {
                if (context.Request.Path.Value.Contains("invalid"))
                {
                    throw new Exception("ERROR!");
                }
                await next();
            });

            app.UseIdentity();


            app.UseMvc(routes =>
            {
                routes.MapRoute("Default",
                    "{controller=Home}/{action=Index}/{id?}"
                    );
            });

            app.UseFileServer();
        }
    }
}
