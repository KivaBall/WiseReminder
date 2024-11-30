﻿namespace WiseReminder.WebAPI.Validators;

public sealed class UpdateQuoteRequestValidator : AbstractValidator<UpdateQuoteRequest>
{
    public UpdateQuoteRequestValidator()
    {
        RuleFor(q => q.Id)
            .NotNull().WithMessage("ID must not be null");

        RuleFor(q => q.Text)
            .NotNull().WithMessage("Text must not be null")
            .NotEmpty().WithMessage("Text must not be empty");

        RuleFor(q => q.AuthorId)
            .NotNull().WithMessage("Author ID must not be null");

        RuleFor(q => q.CategoryId)
            .NotNull().WithMessage("Category ID must not be null");

        RuleFor(q => q.QuoteDate)
            .NotNull().WithMessage("Quote date must not be null");
    }
}