namespace WiseReminder.Application.Quotes.Queries.GetQuoteDtoById;

public sealed record GetQuoteDtoByIdQuery(Guid Id, string? DesiredLanguage) : IQuery<QuoteDto>;