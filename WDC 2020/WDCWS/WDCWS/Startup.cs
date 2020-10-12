using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using WDCWS.Data;

namespace WDCWS
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
            services.AddRazorPages(); // (MVC) .cshtml Support
            services.AddServerSideBlazor(); // Blazor Server Side Support
            services.AddSingleton<WeatherForecastService>(); // Nur für dieses Template notwendig



            services.AddTransient<IPersonQuery, PersonServerSideQuery>();  // Für jede Anfrage / Komponente eine eigene Instanz
            //services.AddScoped<IPersonQuery, PersonServerSideQuery>();     // Für jede Blazor "Sitzung"
            //services.AddSingleton<IPersonQuery, PersonServerSideQuery>();  // 1 Instanz für die "gesamte Anwendung"
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapBlazorHub();
                endpoints.MapFallbackToPage("/_Host");
            });
        }
    }
}
