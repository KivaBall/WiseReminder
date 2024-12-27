namespace WiseReminder.Application.Quotes.DeleteQuote;

public sealed record DeleteQuoteAsAdminCommand : ICommand
{
    public required Guid Id { get; init; }
}