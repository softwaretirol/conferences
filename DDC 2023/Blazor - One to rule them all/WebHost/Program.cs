var builder = WebApplication.CreateBuilder(args);
builder.Services.AddServerSideBlazor();
builder.Services.AddRazorPages();

var app = builder.Build();
app.MapBlazorHub();
app.UseBlazorFrameworkFiles();
app.MapFallbackToPage("/_Host");
app.Run();