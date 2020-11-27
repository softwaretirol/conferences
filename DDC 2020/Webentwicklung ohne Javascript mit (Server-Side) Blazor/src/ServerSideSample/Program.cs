using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace ServerSideSample
{
    class Program
    {
        static void Main(string[] args)
        {
            Host.CreateDefaultBuilder()
                .ConfigureWebHostDefaults(x =>
                {
                    x.UseStartup<Startup>();
                })
                .Build()
                .Run();
        }
    }

    internal class Startup
    {
        public void ConfigureServices(IServiceCollection serviceCollection)
        {
            // Dependency Injection Konfiguration
            // DI-Container
            // 1. Phase Registrierung (Register) - IServiceCollection
            // 2. Phase Auflösung (Resolve) - IServiceProvider

            // serviceCollection.AddTransient<IConfiguration, Configuration>(); <-- Jede Anfrage baut eine neue Instanz
            // serviceCollection.AddScoped<>(); <-- "Scope" / Bereich
            // serviceCollection.AddSingleton<>(); <-- Es gibt nur max. 1 Instanz im Container
            serviceCollection.AddRazorPages();
            serviceCollection.AddServerSideBlazor();
        }

        public void Configure(IApplicationBuilder app,
            IConfiguration configuration, 
            IWebHostEnvironment webHostEnvironment)
        {
            app.UseStaticFiles(); // 1. Middleware
            app.UseRouting();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapBlazorHub();
                endpoints.MapFallbackToPage("/_Host");
            });
        }
    }
}
