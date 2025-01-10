namespace WiseReminder.Application.Quotes.Queries.GetTranslatedQuoteDtos;

public sealed record GetTranslatedQuoteDtosQuery(
    ICollection<QuoteDetails> Quotes,
    string DesiredLanguage)
    : IQuery<ICollection<QuoteDto>>;