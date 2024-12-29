namespace WiseReminder.Application.Quotes.DeleteQuoteAsAdmin;

public sealed record DeleteQuoteAsAdminCommand : ICommand
{
    public required Guid Id { get; init; }
}