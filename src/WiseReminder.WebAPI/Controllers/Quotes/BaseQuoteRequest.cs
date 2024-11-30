namespace WiseReminder.WebAPI.Controllers.Quotes;

public record BaseQuoteRequest
{
    public string? Text { get; init; }
    public Guid? AuthorId { get; init; }
    public Guid? CategoryId { get; init; }
    public DateOnly? QuoteDate { get; init; }
}