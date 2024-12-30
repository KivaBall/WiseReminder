namespace WiseReminder.Application.Quotes.GetQuoteDtosByCategoryId;

public sealed record GetQuoteDtosByCategoryIdQuery(Guid CategoryId) : IQuery<ICollection<QuoteDto>>;