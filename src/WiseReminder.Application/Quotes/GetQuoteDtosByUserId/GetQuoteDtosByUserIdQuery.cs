namespace WiseReminder.Application.Quotes.GetQuoteDtosByUserId;

public sealed record GetQuoteDtosByUserIdQuery(Guid UserId) : IQuery<ICollection<QuoteDto>>;