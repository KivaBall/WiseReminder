namespace WiseReminder.Application.Authors.Commands.CreateAuthorByUser;

public sealed record CreateAuthorByUserCommand : ICommand<Guid>
{
    public required string Name { get; init; }
    public required string Biography { get; init; }
    public required DateOnly BirthDate { get; init; }
    public required Guid UserId { get; init; }
}