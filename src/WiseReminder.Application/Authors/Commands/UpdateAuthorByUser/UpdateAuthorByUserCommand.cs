namespace WiseReminder.Application.Authors.Commands.UpdateAuthorByUser;

public sealed record UpdateAuthorByUserCommand : ICommand
{
    public required string Name { get; init; }
    public required string Biography { get; init; }
    public required DateOnly BirthDate { get; init; }
    public required Guid UserId { get; init; }
}