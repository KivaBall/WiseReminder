namespace WiseReminder.Infrastructure.JWT;

public sealed class JwtService(IConfiguration configuration) : IJwtService
{
    public string GenerateJwtTokenForUser(Guid userId)
    {
        var jwtPassword = configuration["JWTPassword"] ??
                          throw new Exception("JWTPassword isn't in appsettings.json");

        var key = Encoding.UTF8.GetBytes(jwtPassword);

        var claims = new Dictionary<string, object>
        {
            { "UserId", userId.ToString() }
        };

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Issuer = "WiseReminder.com",
            Audience = "WiseReminder.com",
            Expires = DateTime.UtcNow.AddHours(1),
            SigningCredentials =
                new SigningCredentials(new SymmetricSecurityKey(key),
                    SecurityAlgorithms.HmacSha256Signature),
            Claims = claims,
            Subject = new ClaimsIdentity("User")
        };

        var tokenHandler = new JwtSecurityTokenHandler();

        var token = tokenHandler.CreateToken(tokenDescriptor);

        return tokenHandler.WriteToken(token);
    }

    public string GenerateJwtTokenForAdmin()
    {
        var jwtPassword = configuration["JWTPassword"] ??
                          throw new Exception("JWTPassword isn't in appsettings.json");

        var key = Encoding.UTF8.GetBytes(jwtPassword);

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Issuer = "WiseReminder.com",
            Audience = "WiseReminder.com",
            Expires = DateTime.UtcNow.AddHours(1),
            SigningCredentials =
                new SigningCredentials(new SymmetricSecurityKey(key),
                    SecurityAlgorithms.HmacSha256Signature),
            Subject = new ClaimsIdentity("Admin")
        };

        var tokenHandler = new JwtSecurityTokenHandler();

        var token = tokenHandler.CreateToken(tokenDescriptor);

        return tokenHandler.WriteToken(token);
    }
}