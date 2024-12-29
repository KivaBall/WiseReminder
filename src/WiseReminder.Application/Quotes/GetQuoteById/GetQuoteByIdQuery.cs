namespace WiseReminder.Application.Quotes.GetQuoteById;

public sealed record GetQuoteByIdQuery : IQuery<Quote>
{
    public required Guid Id { get; init; }
}