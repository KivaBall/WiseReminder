namespace WiseReminder.WebAPI.Validators.Authors;

public sealed class BaseAuthorAsUserRequestValidator : AbstractValidator<BaseAuthorAsUserRequest>
{
    public BaseAuthorAsUserRequestValidator()
    {
        RuleFor(r => r.Name)
            .NotNull().WithMessage("Name must not be null")
            .NotEmpty().WithMessage("Name must not be empty");

        RuleFor(r => r.Biography)
            .NotNull().WithMessage("Biography must not be null")
            .NotEmpty().WithMessage("Biography must not be empty");

        RuleFor(r => r.BirthDate)
            .NotEmpty().WithMessage("Date of birth must not be empty");
    }
}