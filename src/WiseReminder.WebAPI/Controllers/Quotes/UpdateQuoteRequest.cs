namespace WiseReminder.WebAPI.Controllers.Quotes;

public sealed record UpdateQuoteRequest : BaseQuoteRequest
{
    public required Guid Id { get; init; }
}