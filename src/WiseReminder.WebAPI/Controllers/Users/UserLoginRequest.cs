namespace WiseReminder.WebAPI.Controllers.Users;

public sealed record UserLoginRequest
{
    public required string Login { get; init; }
    public required string Password { get; init; }
}