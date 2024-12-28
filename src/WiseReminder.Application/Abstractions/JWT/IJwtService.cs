namespace WiseReminder.Application.Abstractions.JWT;

public interface IJwtService
{
    public string GenerateJwtTokenForUser(Guid userId);
    public string GenerateJwtTokenForAdmin();
}