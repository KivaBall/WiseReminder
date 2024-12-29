namespace WiseReminder.WebAPI.Validators.Quotes;

public sealed class BaseQuoteAsAdminRequestValidator : AbstractValidator<BaseQuoteAsAdminRequest>
{
    public BaseQuoteAsAdminRequestValidator()
    {
        RuleFor(r => r.Text)
            .NotNull().WithMessage("Text must not be null")
            .NotEmpty().WithMessage("Text must not be empty");

        RuleFor(r => r.AuthorId)
            .NotNull().WithMessage("Author ID must not be null");

        RuleFor(r => r.CategoryId)
            .NotNull().WithMessage("Category ID must not be null");

        RuleFor(r => r.QuoteDate)
            .NotNull().WithMessage("Quote date must not be null");
    }
}