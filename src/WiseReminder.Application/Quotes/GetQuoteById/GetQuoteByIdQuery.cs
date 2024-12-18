namespace WiseReminder.Application.Quotes.GetQuoteById;

public sealed record GetQuoteByIdQuery : IQuery<Quote>
{
    public Guid Id { get; init; }
}