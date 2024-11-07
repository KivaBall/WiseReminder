using WiseReminder.Application.Abstractions.MediatR;
using WiseReminder.Application.Categories.GetAllCategories;
using WiseReminder.Application.Categories;
using WiseReminder.Domain.Categories;

namespace WiseReminder.Application.Authors.GetAllAuthors;

public sealed record GetAllAuthorsQuery : IQuery<ICollection<AuthorVm>>;