using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SantexLeague.API.LogMiddleware;
using SantexLeague.DataAccess.DependencyInjection;
using SantexLeague.DataAccess.EntityFramework;
using SantexLeague.Integration.DependencyInjection;
using SantexLeague.Services.DependencyInjection;

namespace ImportLeague.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
            services.AddHttpClient();
            services.AddDbContext<SantexContext>(
                options => options.UseSqlServer(
                     Configuration.GetConnectionString("SantexDbConnection")));
            services.AddIntegrationLibrary();
            services.AddServiceLibrary();
            services.AddDataAccessLibrary();
            services.AddSwaggerDocument(config =>
            {
                config.PostProcess = document =>
                {
                    document.Info.Version = "v1";
                    document.Info.Title = "Santex Interview API";
                    document.Info.Description = "API for importing football leagues and storing them locally for later queries";
                    document.Info.Contact = new NSwag.OpenApiContact
                    {
                        Name = "Denis Ariel Montaña",
                        Email = "arielmon.lr@gmail.com",
                    };
                };
            });
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            using (var serviceScope = app.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetRequiredService<SantexContext>();
                context.Database.EnsureCreated();
            }
            app.UseMiddleware<LogMiddleware>();
            app.UseStaticFiles();
            app.UseOpenApi();
            app.UseSwaggerUi3();
            app.UseMvc();
        }
    }
}