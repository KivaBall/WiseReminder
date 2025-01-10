namespace WiseReminder.Application.Quotes.Queries.GetQuoteDtosByUserId;

public sealed record GetQuoteDtosByUserIdQuery(Guid UserId) : IQuery<ICollection<QuoteDto>>;