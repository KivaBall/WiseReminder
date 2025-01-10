namespace WiseReminder.Application.Quotes.Queries.GetQuotes;

public sealed record GetQuotesQuery : IQuery<ICollection<Quote>>
{
    public required Guid? CategoryId { get; init; }
    public required Guid? AuthorId { get; init; }
    public required ICollection<string>? Keywords { get; init; }
}