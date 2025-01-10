namespace WiseReminder.Application.Quotes.Queries.GetRandomQuoteDtos;

public sealed record GetRandomQuoteDtosQuery(int Amount, string? DesiredLanguage)
    : IQuery<ICollection<QuoteDto>>;