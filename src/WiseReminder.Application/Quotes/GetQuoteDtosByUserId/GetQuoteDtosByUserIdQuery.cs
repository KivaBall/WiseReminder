namespace WiseReminder.Application.Quotes.GetQuoteDtosByUserId;

public sealed record GetQuoteDtosByUserIdQuery : IQuery<ICollection<QuoteDto>>
{
    public required Guid UserId { get; init; }
}