using System;
using DwxBlazor.RazorLib.Contracts;
using DwxBlazor.Server.Implementations;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

Console.WriteLine("Starte Server");

Host.CreateDefaultBuilder()
    .ConfigureWebHostDefaults(x =>
    {
        x.UseStartup<Startup>();
    })
    .Build()
    .Run();



public class Startup
{
    public void ConfigureServices(IServiceCollection serviceCollection)
    {
        //serviceCollection.AddTransient<>();
        //serviceCollection.AddScoped<>()
        //serviceCollection.AddSingleton<>()

        serviceCollection.AddTransient<ICustomerService, DemoCustomerService>();

        serviceCollection.AddServerSideBlazor();
        serviceCollection.AddRazorPages();
    }

    public void Configure(IConfiguration configuration,
        IApplicationBuilder appBuilder)
    {
        appBuilder.UseStaticFiles();
        appBuilder.UseRouting();
        appBuilder.UseEndpoints(x =>
        {
            x.MapBlazorHub();
            x.MapFallbackToPage("/_Host");
        });
    }
}