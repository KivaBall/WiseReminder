namespace WiseReminder.WebAPI.Controllers.Quotes;

public sealed record GetQuotesRequest
{
    public required Guid? AuthorId { get; init; }
    public required Guid? CategoryId { get; init; }
    public required List<string>? Keywords { get; init; }
    public required string? DesiredLanguage { get; init; }
}