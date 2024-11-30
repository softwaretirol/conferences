using NET9.Blazor.Components;

var builder = WebApplication.CreateBuilder(args);

builder
    .Services
    .AddRazorComponents()
    .AddInteractiveServerComponents();
var app = builder.Build();
app.UseHttpsRedirection();
app.UseAntiforgery();
app.MapStaticAssets();
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();
app.Run();