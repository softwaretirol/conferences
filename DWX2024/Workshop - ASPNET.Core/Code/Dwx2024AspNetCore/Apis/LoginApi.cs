using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace Dwx2024AspNetCore.Apis;

public static class LoginApi
{
    public static IEndpointRouteBuilder MapLoginApi(this IEndpointRouteBuilder app)
    {
        var group = app
            .MapGroup("/identity")
            .AllowAnonymous();
        group.MapPost("/login", (LoginData data, HttpContext http, IConfiguration configuration) =>
        {
            if (data.Password == "supersafe")
            {
                var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(
                    configuration["JwtKey"]));
                var cred = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);
                var token = new JwtSecurityToken(
                    claims: new[]
                    {
                        new Claim("userid", Guid.Empty.ToString())
                    },
                    issuer: "DWX2024",
                    audience:"DWX2024",
                    expires: DateTime.UtcNow.AddDays(1),
                    signingCredentials: cred
                );
                var jwt = new JwtSecurityTokenHandler().WriteToken(token);
                return Results.Ok(jwt);
            }

            return Results.Forbid();
        });
        return app;
    }

    public class LoginData
    {
        public string Mail { get; set; }
        public string Password { get; set; }
    }
}