namespace WiseReminder.Application.Users.ChangePassword;

public sealed record ChangePasswordCommand : ICommand
{
    public required Guid Id { get; init; }
    public required string OldPassword { get; init; }
    public required string NewPassword { get; init; }
};