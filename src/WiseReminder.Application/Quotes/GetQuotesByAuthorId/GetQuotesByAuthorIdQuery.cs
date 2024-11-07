using WiseReminder.Application.Abstractions.MediatR;
using WiseReminder.Application.Categories;
using WiseReminder.Application.Quotes.GetQuoteById;

namespace WiseReminder.Application.Quotes.GetQuotesByAuthorId;

public sealed record GetQuotesByAuthorIdQuery(Guid AuthorId) : IQuery<ICollection<QuoteVm>>;