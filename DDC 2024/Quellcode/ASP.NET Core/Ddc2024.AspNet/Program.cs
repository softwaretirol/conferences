using System.Diagnostics;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Ddc2024.AspNet;
using Microsoft.IdentityModel.Tokens;

WebApplicationBuilder builder = WebApplication.CreateBuilder();
// Dependency Injection - 1. Phase IServiceCollection
var services = builder.Services;
services.AddScoped<MyRegisteredClass>();
// services.AddSingleton<IMyRegisteredClass, MyRegisteredClass>();
builder.Services.AddOpenApi();
builder.Services.AddAuthorization();
var symmetricSecurityKey =
    new SymmetricSecurityKey(Encoding.UTF8.GetBytes("123456781234567812345678123456781234567812345678"));

builder.Services
    .AddAuthentication()
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters.ValidateActor = false;
        options.TokenValidationParameters.ValidateIssuer = false;
        options.TokenValidationParameters.ValidateAudience = false;
        options.TokenValidationParameters.ValidateLifetime = false;
        
        options.TokenValidationParameters.IssuerSigningKey =
            symmetricSecurityKey;
    });


// JWT

// OpenAPI Standard


// builder.Services.ConfigureHttpJsonOptions(options =>
// {
// });
WebApplication app = builder.Build(); // 2. Phase IServiceProvider
// HTTP GET "/Customer"
// var instance1 = app.Services.GetRequiredService<MyRegisteredClass>();
// var instance2 = app.Services.GetRequiredService<MyRegisteredClass>();

// ApiController
// Minimal API


app.UseAuthentication();
app.UseAuthorization();
app.MapOpenApi();
app.MapDdcApi();

// DONT DO THAT AT HOME!
app.MapGet("/token", (string username, string password) =>
{
    var handler = new JwtSecurityTokenHandler();
    var securityTokenDescriptor = new SecurityTokenDescriptor();
    securityTokenDescriptor.Claims = new Dictionary<string, object>
    {
        {"conference", "DDC2024"}
    };
    securityTokenDescriptor.SigningCredentials =
        new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);
    var token = handler.CreateJwtSecurityToken(securityTokenDescriptor);
    
    var tokenString = handler.WriteToken(token);
    return tokenString;
});

// app.MapCustomerApi();
app.MapGet("/customers2/{customerId}/", CustomerMinimalApi.GetCustomer);

app.MapGet("/customers/{customerId}/",
    (int customerId, HttpContext http, MyRegisteredClass instance, IConfiguration configuration) =>
    {
        var db = configuration["DB"];
        var property = configuration["Config:Property"];

        return "Hello World";
    }).RequireAuthorization();

// Pipeline
// app.Use(async (HttpContext context, Func<Task> next) =>
// {
//     var watch = Stopwatch.StartNew();
//     await context.Response.WriteAsJsonAsync("Hello World");
//     watch.Stop();
// });
await app.RunAsync();

class MyRegisteredClass
{
    public Guid Id { get; set; } = Guid.NewGuid();
}