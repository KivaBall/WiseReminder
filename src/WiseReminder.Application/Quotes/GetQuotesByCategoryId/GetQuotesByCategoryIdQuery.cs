namespace WiseReminder.Application.Quotes.GetQuotesByCategoryId;

public sealed record GetQuotesByCategoryIdQuery : IQuery<ICollection<QuoteDto>>
{
    public Guid CategoryId { get; init; }
}