namespace WiseReminder.WebAPI.Validators.Users;

public sealed class ChangeUsernameRequestValidator : AbstractValidator<ChangeUsernameRequest>
{
    public ChangeUsernameRequestValidator()
    {
        RuleFor(r => r.NewUsername)
            .NotNull().WithMessage("New username must not be null")
            .NotEmpty().WithMessage("New username must not be empty");

        RuleFor(r => r.Password)
            .NotNull().WithMessage("Password must not be null")
            .NotEmpty().WithMessage("Password must not be empty");
    }
}