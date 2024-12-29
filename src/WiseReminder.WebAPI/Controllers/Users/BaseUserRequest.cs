namespace WiseReminder.WebAPI.Controllers.Users;

public sealed record BaseUserRequest
{
    public required string Username { get; init; }
    public required string Login { get; init; }
    public required string Password { get; init; }
}