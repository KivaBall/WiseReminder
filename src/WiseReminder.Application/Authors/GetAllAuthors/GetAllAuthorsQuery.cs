using WiseReminder.Application.Abstractions.MediatR;

namespace WiseReminder.Application.Authors.GetAllAuthors;

public sealed record GetAllAuthorsQuery : IQuery<ICollection<AuthorVm>>;