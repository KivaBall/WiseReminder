namespace WiseReminder.Application.Users.Commands.LoginAsAdmin;

public sealed record LoginAsAdminCommand : ICommand<string>
{
    public required string FirstPassword { get; init; }
    public required string SecondPassword { get; init; }
    public required string ThirdPassword { get; init; }
}