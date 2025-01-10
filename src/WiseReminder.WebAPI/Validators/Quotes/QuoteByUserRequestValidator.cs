namespace WiseReminder.WebAPI.Validators.Quotes;

public sealed class QuoteByUserRequestValidator : AbstractValidator<QuoteByUserRequest>
{
    public QuoteByUserRequestValidator()
    {
        RuleFor(r => r.Text)
            .NotNull().WithMessage("Text must not be null")
            .NotEmpty().WithMessage("Text must not be empty");

        RuleFor(r => r.CategoryId)
            .NotEmpty().WithMessage("Category ID must not be empty");

        RuleFor(r => r.QuoteDate)
            .NotEmpty().WithMessage("Quote date must not be empty");
    }
}