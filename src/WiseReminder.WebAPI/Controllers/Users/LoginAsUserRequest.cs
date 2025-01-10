namespace WiseReminder.WebAPI.Controllers.Users;

public sealed record LoginAsUserRequest
{
    public required string Login { get; init; }
    public required string Password { get; init; }
}