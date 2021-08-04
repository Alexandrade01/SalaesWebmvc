using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Localization;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Globalization;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SalesWebMVC.Models;
using SalesWebMVC.Data;
using SalesWebMVC.Services;

namespace SalesWebMVC
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
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });


            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
            /* COLOQUE o endereço SQL primeiro no appsetting.json
             * "SalesWebMvcContext" é a classe entity criada
             * "SalesWebMVC" é o nome do projeto */

            services.AddDbContext<SalesWebMVCContext>(options =>
        options.UseMySql(Configuration.GetConnectionString("SalesWebMvcContext"), builder =>
builder.MigrationsAssembly("SalesWebMVC")));


            //dependency injection system
            services.AddScoped<SeedingService>();
            services.AddScoped<SellerService>();
            services.AddScoped<DepartmentService>();


        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, SeedingService seedingService)
        {
            var enUs = new CultureInfo("en-US");
            var localizationOptions = new RequestLocalizationOptions
            {
                DefaultRequestCulture = new RequestCulture(enUs),
                SupportedCultures = new List<CultureInfo> { enUs },
                SupportedUICultures = new List<CultureInfo> { enUs }
            };
            app.UseRequestLocalization(localizationOptions);

            // if project is development 
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();

                //Seeding data 
                seedingService.Seed();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
