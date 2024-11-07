using WiseReminder.Application.Abstractions.MediatR;
using WiseReminder.Application.Authors.GetAuthorById;
using WiseReminder.Application.Authors;
using WiseReminder.Domain.Authors;

namespace WiseReminder.Application.Quotes.GetQuoteById;

public sealed record GetQuoteByIdQuery(Guid Id) : IQuery<QuoteVm>;