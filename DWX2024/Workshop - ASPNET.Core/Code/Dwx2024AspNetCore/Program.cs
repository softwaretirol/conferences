using System.Diagnostics;
using System.Text;
using Dwx2024AspNetCore.Apis;
using Dwx2024AspNetCore.Middlewares;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc.TagHelpers.Cache;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

WebApplicationBuilder builder = WebApplication.CreateBuilder();
builder
    .Services
    .AddSwaggerGen(x =>
    {
        var path = typeof(Program).Assembly.Location;
        var xmlFile = Path.ChangeExtension(path, "xml");
        x.IncludeXmlComments(xmlFile);
        x.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
        {
            Description = "JWT Authorization header using the Bearer scheme. \r\n\r\n Enter 'Bearer' [space] and then your token in the text input below.\r\n\r\nExample: \"Bearer 12345abcdef\"",
            Name = "Authorization",
            In = ParameterLocation.Header,
            Type = SecuritySchemeType.ApiKey
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
    })
    .AddOpenApiDocument()
    .AddEndpointsApiExplorer()
    
    .AddAuthorization()
    .AddAuthentication()
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidIssuers = ["DWX2024"],
            ValidateActor = false,
            ValidateAudience = false,
            ValidateLifetime = false,
            ValidateIssuer = false,
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JwtKey"]))
        };
    })
    ;
// builder.WebHost.UseUrls("https://localhost:12345");
WebApplication app = builder.Build();

// app.MapWhen(x => x.Request.Path.StartsWithSegments("/api/person"),
//     api =>
//     {
//         api.UseMiddleware<StopwatchMiddleware>();
//
//     });

// Swagger.json - API Spezifikation
// OpenAPI

app.UseMiddleware<StopwatchMiddleware>();
app.UseAuthentication();
app.UseAuthorization();
app.UseSwagger(); // OpenApi
app.UseSwaggerUI();

app.MapMyApi();

// app.MapSwagger()
// Func<string, int> a = (string s) => { return 1; };

// APIs mit Microsoft - API Controllers, Minimal APIs
// * API Controllers - ähnlich zu .NET FX Class : ControllerBase
// * Minimal APIs - "Funktionaller" Ansatz mit Delegates

// app.MapGet("/api/person/{id}", (int id) =>
// {
//     return new
//     {
//         Vorname = "Christian " + id,
//         Nachname = "Giesswein"
//     };
// });
    
    
    
// app.Use(async (HttpContext ctx, Func<Task> next) =>
// {
//     // ctx.Request.
//     // await next();
//     // 
//     ctx.Response.StatusCode = 200;
//     await ctx.Response.WriteAsJsonAsync("Hallo");
// });

if (app.Environment.IsDevelopment())
{
    
}

app.Run();