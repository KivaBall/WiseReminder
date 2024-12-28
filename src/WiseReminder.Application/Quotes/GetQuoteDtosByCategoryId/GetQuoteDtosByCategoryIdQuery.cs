namespace WiseReminder.Application.Quotes.GetQuoteDtosByCategoryId;

public sealed record GetQuoteDtosByCategoryIdQuery : IQuery<ICollection<QuoteDto>>
{
    public required Guid CategoryId { get; init; }
}