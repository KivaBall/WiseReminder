namespace WiseReminder.Application.Quotes.DeleteQuote;

public sealed record DeleteQuoteCommand : ICommand
{
    public Guid Id { get; init; }
}