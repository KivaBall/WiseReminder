﻿namespace WiseReminder.Application.Categories.Commands.CreateCategory;

public sealed record CreateCategoryCommand : ICommand<Guid>
{
    public required string Name { get; init; }
    public required string Description { get; init; }
}