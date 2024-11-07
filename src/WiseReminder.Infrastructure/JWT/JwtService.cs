using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using WiseReminder.Application.Abstractions.JWT;

namespace WiseReminder.Infrastructure.JWT;

public class JwtService : IJwtService
{
    public string GenerateJwtToken()
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = "ThisIsSpecialSecurityKeyThisIsSpecialSecurityKeyThisIsSpecialSecurityKeyThisIsSpecialSecurityKey"u8
            .ToArray();

        var tokenDescriptor = new SecurityTokenDescriptor
        {
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