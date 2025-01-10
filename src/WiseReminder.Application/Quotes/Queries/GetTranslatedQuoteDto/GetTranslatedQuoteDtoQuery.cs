namespace WiseReminder.Application.Quotes.Queries.GetTranslatedQuoteDto;

public sealed record GetTranslatedQuoteDtoQuery(QuoteDetails Quote, string DesiredLanguage)
    : IQuery<QuoteDto>;