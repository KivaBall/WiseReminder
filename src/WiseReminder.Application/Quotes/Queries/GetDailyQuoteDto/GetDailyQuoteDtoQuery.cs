namespace WiseReminder.Application.Quotes.Queries.GetDailyQuoteDto;

public sealed record GetDailyQuoteDtoQuery(string? DesiredLanguage) : IQuery<QuoteDto>;