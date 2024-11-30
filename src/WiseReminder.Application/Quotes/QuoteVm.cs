namespace WiseReminder.Application.Quotes;

//TODO: Rename from Vm to Dto
public sealed record QuoteVm(Guid Id, string Text, Guid AuthorId, Guid CategoryId, DateOnly QuoteDate);