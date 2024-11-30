namespace WiseReminder.WebAPI.Validators;

public sealed class BaseCategoryRequestValidator : AbstractValidator<BaseCategoryRequest>
{
    public BaseCategoryRequestValidator()
    {
        RuleFor(c => c.Name)
            .NotNull().WithMessage("Name must not be null")
            .NotEmpty().WithMessage("Name must not be empty");

        RuleFor(c => c.Description)
            .NotNull().WithMessage("Description must not be null")
            .NotEmpty().WithMessage("Description must not be empty");
    }
}