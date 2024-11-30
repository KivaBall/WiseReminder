namespace WiseReminder.WebAPI.Controllers.Authorization;

public sealed record SignInRequest
{
    public string? Login { get; init; }
    public string? Password { get; init; }
}