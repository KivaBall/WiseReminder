namespace WiseReminder.Application.Quotes.GetQuoteDtoById;

public sealed record GetQuoteDtoByIdQuery : IQuery<QuoteDto>
{
    public Guid Id { get; init; }
}