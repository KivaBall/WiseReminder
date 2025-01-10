namespace WiseReminder.WebAPI.Controllers.Users;

public sealed record UpdateUserRequest
{
    public required string OldPassword { get; init; }
    public required string? NewUsername { get; init; }
    public required string? NewPassword { get; init; }
}