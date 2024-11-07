using WiseReminder.Application.Abstractions.MediatR;

namespace WiseReminder.Application.Authors.GetAuthorById;

public sealed record GetAuthorByIdQuery(Guid Id) : IQuery<AuthorVm>;