namespace WiseReminder.Application.Quotes.Queries.GetMonthlyQuoteDto;

public sealed record GetMonthlyQuoteDtoQuery(string? DesiredLanguage) : IQuery<QuoteDto>;