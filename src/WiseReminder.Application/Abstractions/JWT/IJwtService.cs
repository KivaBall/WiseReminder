namespace WiseReminder.Application.Abstractions.JWT;

public interface IJwtService
{
    public string GenerateTokenForUser(Guid userId);
    public string GenerateTokenForAdmin();
}