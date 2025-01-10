namespace WiseReminder.Infrastructure.JWT;

public sealed class JwtService(IConfiguration configuration) : IJwtService
{
    public string GenerateTokenForUser(Guid userId)
    {
        var claims = new Dictionary<string, object>
        {
            { ClaimTypes.Role, "User" },
            { "UserId", userId.ToString() }
        };

        return GenerateToken(claims);
    }

    public string GenerateTokenForAdmin()
    {
        var claims = new Dictionary<string, object>
        {
            { ClaimTypes.Role, "Admin" }
        };

        return GenerateToken(claims);
    }

    private string GenerateToken(Dictionary<string, object> claims)
    {
        var jwtPassword = configuration["JWTPassword"] ??
                          throw new InvalidOperationException("JWTPassword is not configured");

        var key = Encoding.UTF8.GetBytes(jwtPassword);

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Issuer = "WiseReminder.com",
            Audience = "WiseReminder.com",
            Expires = DateTime.UtcNow.AddHours(72),
            SigningCredentials = new SigningCredentials(
                new SymmetricSecurityKey(key),
                SecurityAlgorithms.HmacSha256Signature),
            Claims = claims
        };

        var tokenHandler = new JwtSecurityTokenHandler();

        var token = tokenHandler.CreateToken(tokenDescriptor);

        return tokenHandler.WriteToken(token);
    }
}