﻿namespace WiseReminder.WebAPI.Validators.Categories;

public sealed class CategoryRequestValidator : AbstractValidator<CategoryRequest>
{
    public CategoryRequestValidator()
    {
        RuleFor(r => r.Name)
            .NotNull().WithMessage("Name must not be null")
            .NotEmpty().WithMessage("Name must not be empty");

        RuleFor(r => r.Description)
            .NotNull().WithMessage("Description must not be null")
            .NotEmpty().WithMessage("Description must not be empty");
    }
}