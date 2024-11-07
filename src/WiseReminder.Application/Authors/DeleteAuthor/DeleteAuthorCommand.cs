using WiseReminder.Application.Abstractions.MediatR;
using WiseReminder.Application.Categories.DeleteCategory;
using WiseReminder.Domain.Categories;

namespace WiseReminder.Application.Authors.DeleteAuthor;

public sealed record DeleteAuthorCommand(Guid Id) : ICommand;