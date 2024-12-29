namespace WiseReminder.WebAPI.Controllers.Users;

public sealed record UpdatePasswordRequest
{
    public required string OldPassword { get; init; }
    public required string NewPassword { get; init; }
}