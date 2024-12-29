namespace WiseReminder.Application.Quotes.GetQuoteDtoById;

public sealed record GetQuoteDtoByIdQuery : IQuery<QuoteDto>
{
    public required Guid Id { get; init; }
}