namespace WiseReminder.Application.Quotes.GetQuotesByAuthorId;

public sealed record GetQuotesByAuthorIdQuery(Guid AuthorId) : IQuery<ICollection<QuoteDto>>;