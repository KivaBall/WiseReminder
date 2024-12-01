namespace WiseReminder.Application.Quotes.GetQuotesByCategoryId;

public sealed record GetQuotesByCategoryIdQuery(Guid CategoryId) : IQuery<ICollection<QuoteDto>>;