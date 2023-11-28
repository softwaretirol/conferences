using BlazorUI;
using BlazorUI.Contracts;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<HelloWorld>("#app");

builder.Services.AddTransient<IPersonDataSource, MyApiPersonDataSource>();

await builder.Build().RunAsync();