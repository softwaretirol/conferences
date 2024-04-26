using System.ComponentModel.DataAnnotations;
using System.IdentityModel.Tokens.Jwt;
using System.Reflection;
using System.Reflection.Metadata;
using System.Security.Claims;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace DDF2024;

public static class DataAccessLayerServices
{
    public static IServiceCollection AddDataAccessLayer
        (this IServiceCollection services)
    {
        services.AddSingleton<MyMiddleware>();
        services.AddTransient<DataAccessLayer>();
        return services;
    }
}

public static class TokenApi
{
    public static IEndpointRouteBuilder AddTokenApi
        (this IEndpointRouteBuilder app)
    {
        app.MapPost("/token", (LoginModel model, IConfiguration _configuration) =>
        {
            if (model.Password == "123")
            {
               var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(
                    _configuration.GetSection("AppSettings:Token").Value));
                var cred = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);
                var token = new JwtSecurityToken(
                    claims: new []
                    {
                        new Claim("userid", Guid.Empty.ToString())
                    },
                    expires: DateTime.UtcNow.AddDays(1),
                    signingCredentials: cred
                );
                var jwt = new JwtSecurityTokenHandler().WriteToken(token);
                return Results.Ok(jwt);
            }

            return Results.Unauthorized();
        });
        return app;
    }
}

public class LoginModel
{
    public string Mail { get; set; }
    public string Password { get; set; }
}

public static class DataAccessLayerApi
{
    public static IEndpointRouteBuilder AddDataAccessLayerApi
        (this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/customer")
            .RequireAuthorization();
        
        group.MapGet("/GetNames", GetNames);
        group.MapGet("/Http/{number}", (int number) =>
        {
            if (number % 2 == 0)
            {
                return Results.Ok("Everything alright");
            }
            
            return Results.NotFound();
        });
        
        group.MapPost("/add/{parameter}", 
            async (Person p, int parameter, DataAccessLayer dal) =>
        {
            await dal.Add(p);
        });
        return app;
    }

    private static List<string> GetNames(DataAccessLayer dal, HttpContext context)
    {
        var user = context.User;
        return dal.GetNames();
    }
}

public class Person
{
    [Required]
    public string FirstName { get; set; }
    public string LastName { get; set; }
}