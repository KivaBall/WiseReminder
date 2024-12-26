namespace WiseReminder.Application.Quotes.GetQuotesByCategoryId;

public sealed record GetQuotesByCategoryIdQuery : IQuery<ICollection<QuoteDto>>
{
    public required Guid CategoryId { get; init; }
}