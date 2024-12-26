namespace WiseReminder.Application.Quotes.DeleteQuote;

public sealed record DeleteQuoteCommand : ICommand
{
    public required Guid Id { get; init; }
}