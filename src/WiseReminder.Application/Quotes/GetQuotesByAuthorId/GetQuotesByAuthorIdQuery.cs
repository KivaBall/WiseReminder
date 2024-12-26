namespace WiseReminder.Application.Quotes.GetQuotesByAuthorId;

public sealed record GetQuotesByAuthorIdQuery : IQuery<ICollection<QuoteDto>>
{
    public required Guid AuthorId { get; init; }
}