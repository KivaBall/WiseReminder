namespace WiseReminder.WebAPI.Validators;

public sealed class BaseAuthorRequestValidator : AbstractValidator<BaseAuthorRequest>
{
    public BaseAuthorRequestValidator()
    {
        RuleFor(a => a.Name)
            .NotNull().WithMessage("Name must not be null")
            .NotEmpty().WithMessage("Name must not be empty");

        RuleFor(a => a.Biography)
            .NotNull().WithMessage("Biography must not be null")
            .NotEmpty().WithMessage("Biography must not be empty");

        RuleFor(a => a.DateOfBirth)
            .NotNull().WithMessage("Date of birth must not be null");

        RuleFor(a => a.DateOfDeath)
            .NotNull().WithMessage("Date of death must not be null");
    }
}