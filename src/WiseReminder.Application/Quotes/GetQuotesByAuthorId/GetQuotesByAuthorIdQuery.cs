using WiseReminder.Application.Abstractions.MediatR;

namespace WiseReminder.Application.Quotes.GetQuotesByAuthorId;

public sealed record GetQuotesByAuthorIdQuery(Guid AuthorId) : IQuery<ICollection<QuoteVm>>;