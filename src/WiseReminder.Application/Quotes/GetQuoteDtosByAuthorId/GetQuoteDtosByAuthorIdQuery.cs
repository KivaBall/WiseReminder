namespace WiseReminder.Application.Quotes.GetQuoteDtosByAuthorId;

public sealed record GetQuoteDtosByAuthorIdQuery(Guid AuthorId) : IQuery<ICollection<QuoteDto>>;