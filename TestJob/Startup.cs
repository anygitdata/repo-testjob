using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using TestJob.Models;
using TestJob.Models.Interface;

namespace TestJob
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

            services.AddSingleton<IAnyUserData, AnyUserData>();

            services.AddDbContext<DataContext>(opts => {
                opts.UseSqlServer(Configuration[
                    "ConnectionStrings:TestJobConnection"]);
                opts.EnableSensitiveDataLogging(true);
            });

            services.AddControllersWithViews();
        }

        
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }
            app.UseStaticFiles();

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                //endpoints.MapControllerRoute(
                //    name: "delComment",
                //    pattern: "del-comment/{id}",
                //    new { Controller = "Home", action = "DelTaskComment" });

                //endpoints.MapControllerRoute(
                //    name: "updComment",
                //    pattern: "upd-comment/{id}",
                //    new { Controller = "Home", action = "UpdTaskComment" });


                endpoints.MapControllerRoute(
                    name: "InsTask",
                    pattern: "instask",
                    new { Controller = "Home", action = "InsTask" });

                endpoints.MapControllerRoute(
                    name: "Filter",
                    pattern: "filter/{dt}",
                    new { Controller = "Home", action = "Filter" });

                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");

                endpoints.MapDefaultControllerRoute();
            });

            SeedData.EnsurePopulated(app);
        }
    }
}
