namespace WiseReminder.Application.Quotes.Queries.GetWeeklyQuoteDto;

public sealed record GetWeeklyQuoteDtoQuery(string? DesiredLanguage) : IQuery<QuoteDto>;