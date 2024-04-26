using DDF2024;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder();
builder.Services.AddDataAccessLayer();
builder.Services
    .AddAuthentication()
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateAudience = false,
            ValidateIssuer = false,
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(
                builder.Configuration.GetSection("AppSettings:Token").Value)),
            ValidateLifetime = true,
        };
    });
builder.Services.AddControllers()
    // .AddApplicationPart(typeof(CustomerController).Assembly)
    ;

builder.Services.AddSwaggerGen(x =>
{
    x.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = "JWT Authorization header using the Bearer scheme. Example: \"Authorization: Bearer {token}\"",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
    });
    x.AddSecurityRequirement(new OpenApiSecurityRequirement {
        { 
            new OpenApiSecurityScheme 
            { 
                Reference = new OpenApiReference 
                { 
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer" 
                } 
            },
            new string[] { } 
        } 
    });
    // x.IncludeXmlComments();
});
builder.Services.AddEndpointsApiExplorer(); // Minimal API Support für Swagger
WebApplication app = builder.Build();
app.AddDataAccessLayerApi()
    .AddTokenApi();
app.MapControllers();
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

await app.RunAsync();

Console.WriteLine("Server ist heruntergefahren");










// --------

// ASP.NET Core Pipeline Configuration
// app.UseMiddleware<MyMiddleware>();
// app.MapWhen(
//     x => x.Request.Path.StartsWithSegments("/asd"),
//     subApp =>
//     {
//         subApp.Use(async (HttpContext context, Func<Task> next) =>
//         {
//             await context.Response.WriteAsJsonAsync("SubApp used...");
//         });
//     });
//
// app.Use(async (HttpContext context, Func<Task> next) =>
// {
//     var dal = context.RequestServices.GetRequiredService<DataAccessLayer>();
//     
//     await context.Response.WriteAsJsonAsync(dal.GetNames());
// });

// --------

public class MyMiddleware : IMiddleware
{
    public Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        return next(context);
    }
}