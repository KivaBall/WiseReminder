using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using WiseReminder.Application.Abstractions.JWT;

namespace WiseReminder.Infrastructure.JWT;

public class JwtService(IConfiguration configuration) : IJwtService
{
    private readonly IConfiguration _configuration = configuration;

    public string GenerateJwtToken()
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.UTF8.GetBytes(_configuration["JWTPassword"] ??
                                         throw new Exception("JWTPassword isn't in appsettings.json"));

        var claims = new ClaimsIdentity();
        claims.AddClaim(new Claim("Email", "DSasd@gmail.com"));
        claims.AddClaim(new Claim("PasswordToken", "ASD83JF9SK"));

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = claims,
            Issuer = "WiseReminder.com",
            Audience = "WiseReminder.com",
            Expires = DateTime.UtcNow.AddHours(1),
            SigningCredentials =
                new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        };

        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }
}