namespace WiseReminder.WebAPI.Controllers.Users;

public sealed record ChangeUsernameRequest
{
    public required string NewUsername { get; init; }
    public required string Password { get; init; }
}