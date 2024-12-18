namespace WiseReminder.Application.Quotes.GetQuotesByAuthorId;

public sealed record GetQuotesByAuthorIdQuery : IQuery<ICollection<QuoteDto>>
{
    public Guid AuthorId { get; init; }
}