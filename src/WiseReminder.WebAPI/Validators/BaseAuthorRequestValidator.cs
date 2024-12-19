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

        RuleFor(a => a.BirthDate)
            .NotEmpty().WithMessage("Date of birth must not be empty");

        RuleFor(a => a.DeathDate)
            .NotEmpty().WithMessage("Date of death must not be empty");
    }
}