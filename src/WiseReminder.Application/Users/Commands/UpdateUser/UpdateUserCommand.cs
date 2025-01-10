namespace WiseReminder.Application.Users.Commands.UpdateUser;

public sealed record UpdateUserCommand : ICommand
{
    public required Guid Id { get; init; }
    public required string OldPassword { get; init; }
    public required string? NewUsername { get; init; }
    public required string? NewPassword { get; init; }
}