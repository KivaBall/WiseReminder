namespace WiseReminder.WebAPI.Validators.Quotes;

public sealed class QuoteByAdminRequestValidator : AbstractValidator<AdminQuoteRequest>
{
    public QuoteByAdminRequestValidator()
    {
        RuleFor(r => r.Text)
            .NotNull().WithMessage("Text must not be null")
            .NotEmpty().WithMessage("Text must not be empty");

        RuleFor(r => r.AuthorId)
            .NotEmpty().WithMessage("Author ID must not be empty");

        RuleFor(r => r.CategoryId)
            .NotEmpty().WithMessage("Category ID must not be empty");

        RuleFor(r => r.QuoteDate)
            .NotEmpty().WithMessage("Quote date must not be empty");
    }
}