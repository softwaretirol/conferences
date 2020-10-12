using System;
using System.Net.Http;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Text;
using DDFWorkshop.UI;
using DDFWorkshop.UI.Contracts;
using DDFWorkshop.WASM.Services;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace BlazorApp6
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("app");

            builder.Services.AddSingleton<ICounterService, ClientSideCounterService>();
            builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

            var webAssemblyHost = builder.Build();
            var counterService = webAssemblyHost.Services.GetService<ICounterService>();
            await ((ClientSideCounterService) counterService).Connect();
            await webAssemblyHost.RunAsync();
        }
    }
}
