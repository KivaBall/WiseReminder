using WiseReminder.Application.Abstractions.MediatR;
using WiseReminder.Application.Quotes.GetQuotesByAuthorId;
using WiseReminder.Domain.Authors;

namespace WiseReminder.Application.Quotes.GetQoutesByCategoryId;

public sealed record GetQuotesByCategoryIdQuery(Guid CategoryId) : IQuery<ICollection<QuoteVm>>;