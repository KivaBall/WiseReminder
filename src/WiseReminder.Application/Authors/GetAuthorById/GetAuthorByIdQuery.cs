using WiseReminder.Application.Abstractions.MediatR;
using WiseReminder.Application.Categories.GetCategoryById;
using WiseReminder.Application.Categories;
using WiseReminder.Domain.Categories;

namespace WiseReminder.Application.Authors.GetAuthorById;

public sealed record GetAuthorByIdQuery(Guid Id) : IQuery<AuthorVm>;