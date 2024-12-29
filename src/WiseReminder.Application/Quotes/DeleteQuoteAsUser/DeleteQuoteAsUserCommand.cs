namespace WiseReminder.Application.Quotes.DeleteQuoteAsUser;

public sealed record DeleteQuoteAsUserCommand : ICommand
{
    public required Guid Id { get; init; }
    public required Guid UserId { get; init; }
}