using Microsoft.Extensions.Caching.Hybrid;
using Microsoft.Extensions.Caching.Memory;

#pragma warning disable EXTEXP0018

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddOpenApi();
builder.Services.AddHybridCache();
var app = builder.Build();
app.MapStaticAssets();
app.MapOpenApi(); // "NEW" Microsoft.AspNetCore.OpenApi
app.UseHttpsRedirection();

app.MapGet("/", (HybridCache cache) =>
{
    return "Hello World";
});
app.Run();