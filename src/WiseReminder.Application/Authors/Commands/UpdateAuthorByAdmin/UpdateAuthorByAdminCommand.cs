namespace WiseReminder.Application.Authors.Commands.UpdateAuthorByAdmin;

public sealed record UpdateAuthorByAdminCommand : ICommand
{
    public required Guid Id { get; init; }
    public required string Name { get; init; }
    public required string Biography { get; init; }
    public required DateOnly BirthDate { get; init; }
    public required DateOnly? DeathDate { get; init; }
}