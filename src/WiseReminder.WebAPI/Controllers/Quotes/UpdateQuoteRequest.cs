namespace WiseReminder.WebAPI.Controllers.Quotes;

public sealed record UpdateQuoteRequest : BaseQuoteRequest
{
    public Guid? Id { get; init; }
}