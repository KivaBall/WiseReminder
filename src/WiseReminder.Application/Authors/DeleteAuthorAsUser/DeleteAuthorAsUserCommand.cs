namespace WiseReminder.Application.Authors.DeleteAuthorAsUser;

public sealed record DeleteAuthorAsUserCommand : ICommand
{
    public Guid UserId { get; init; }
};