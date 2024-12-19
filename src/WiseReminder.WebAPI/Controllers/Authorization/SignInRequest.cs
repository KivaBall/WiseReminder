namespace WiseReminder.WebAPI.Controllers.Authorization;

public sealed record SignInRequest
{
    public required string Login { get; init; }
    public required string Password { get; init; }
}