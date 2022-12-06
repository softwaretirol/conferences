

using Ddc2022Blazor.Rcl.Services;

var builder = WebApplication.CreateBuilder();
builder.Services.AddServerSideBlazor();
builder.Services.AddRazorPages();

//builder.Services.AddTransient<PersonService>();
builder.Services.AddScoped<PersonService>();
//builder.Services.AddSingleton<PersonService>();

var app = builder.Build();
app.UseStaticFiles();
app.MapBlazorHub();
app.MapFallbackToPage("/_Host");
app.Run();

