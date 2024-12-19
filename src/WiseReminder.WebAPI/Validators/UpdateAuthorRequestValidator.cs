namespace WiseReminder.WebAPI.Validators;

public sealed class UpdateAuthorRequestValidator : AbstractValidator<UpdateAuthorRequest>
{
    public UpdateAuthorRequestValidator()
    {
        RuleFor(a => a.Id)
            .NotNull().WithMessage("ID must not be null");

        RuleFor(a => a.Name)
            .NotNull().WithMessage("Name must not be null")
            .NotEmpty().WithMessage("Name must not be empty");

        RuleFor(a => a.Biography)
            .NotNull().WithMessage("Biography must not be null")
            .NotEmpty().WithMessage("Biography must not be empty");

        RuleFor(a => a.BirthDate)
            .NotNull().WithMessage("Date of birth must not be null");

        RuleFor(a => a.DeathDate)
            .NotNull().WithMessage("Date of death must not be null");
    }
}