namespace WiseReminder.Application.Authors;

public sealed record AuthorDto
{
    public required Guid Id { get; init; }
    public required string Name { get; init; }
    public required string Biography { get; init; }
    public required DateOnly BirthDate { get; init; }
    public required DateOnly? DeathDate { get; init; }
    public required int Quotes { get; init; }
    public required DateOnly? MinQuoteDate { get; init; }
    public required DateOnly? MaxQuoteDate { get; init; }
}