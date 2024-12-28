namespace WiseReminder.Application.Quotes.GetQuoteDtosByAuthorId;

public sealed record GetQuoteDtosByAuthorIdQuery : IQuery<ICollection<QuoteDto>>
{
    public required Guid AuthorId { get; init; }
}