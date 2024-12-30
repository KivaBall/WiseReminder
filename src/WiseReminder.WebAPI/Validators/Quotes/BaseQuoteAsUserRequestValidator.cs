namespace WiseReminder.WebAPI.Validators.Quotes;

public sealed class BaseQuoteAsUserRequestValidator : AbstractValidator<UserQuoteRequest>
{
    public BaseQuoteAsUserRequestValidator()
    {
        RuleFor(r => r.Text)
            .NotNull().WithMessage("Text must not be null")
            .NotEmpty().WithMessage("Text must not be empty");

        RuleFor(r => r.CategoryId)
            .NotNull().WithMessage("Category ID must not be null");

        RuleFor(r => r.QuoteDate)
            .NotNull().WithMessage("Quote date must not be null");
    }
}