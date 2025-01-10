namespace WiseReminder.WebAPI.Controllers.Users;

public sealed record LoginAsAdminRequest
{
    public required string FirstPassword { get; init; }
    public required string SecondPassword { get; init; }
    public required string ThirdPassword { get; init; }
}