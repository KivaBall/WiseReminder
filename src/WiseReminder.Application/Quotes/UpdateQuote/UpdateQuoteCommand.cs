using WiseReminder.Application.Abstractions.MediatR;

namespace WiseReminder.Application.Quotes.UpdateQuote;

public sealed record UpdateQuoteCommand : ICommand
{
    public Guid Id { get; init; }
    public string Text { get; init; }
    public Guid AuthorId { get; init; }
    public Guid CategoryId { get; init; }
    public DateOnly QuoteDate { get; init; }
}