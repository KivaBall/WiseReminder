using WiseReminder.Application.Abstractions.MediatR;

namespace WiseReminder.Application.Quotes.CreateQuote;

public sealed record CreateQuoteCommand : ICommand
{
    public string Text { get; init; }
    public Guid AuthorId { get; init; }
    public Guid CategoryId { get; init; }
    public DateOnly QuoteDate { get; init; }
}