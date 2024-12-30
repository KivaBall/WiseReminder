namespace WiseReminder.Application.Quotes.DeleteQuotesByAuthorId;

public sealed record DeleteQuotesByAuthorIdCommand(Guid AuthorId) : ICommand;