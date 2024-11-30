namespace WiseReminder.WebAPI.Validators;

public sealed class UpdateCategoryRequestValidator : AbstractValidator<UpdateCategoryRequest>
{
    public UpdateCategoryRequestValidator()
    {
        RuleFor(c => c.Id)
            .NotNull().WithMessage("ID must not be null");

        RuleFor(c => c.Name)
            .NotNull().WithMessage("Name must not be null")
            .NotEmpty().WithMessage("Name must not be empty");

        RuleFor(c => c.Description)
            .NotNull().WithMessage("Description must not be null")
            .NotEmpty().WithMessage("Description must not be empty");
    }
}