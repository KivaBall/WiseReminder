namespace WiseReminder.Application.Quotes.Queries.GetRecentAddedQuoteDtos;

public sealed record GetRecentAddedQuoteDtosQuery(int Amount, string? DesiredLanguage)
    : IQuery<ICollection<QuoteDto>>;