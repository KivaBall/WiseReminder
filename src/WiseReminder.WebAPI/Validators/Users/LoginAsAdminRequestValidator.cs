namespace WiseReminder.WebAPI.Validators.Users;

public sealed class LoginAsAdminRequestValidator : AbstractValidator<LoginAsAdminRequest>
{
    public LoginAsAdminRequestValidator()
    {
        RuleFor(r => r.FirstPassword)
            .NotNull().WithMessage("Password must not be null")
            .NotEmpty().WithMessage("Password must not be empty");

        RuleFor(r => r.SecondPassword)
            .NotNull().WithMessage("Password must not be null")
            .NotEmpty().WithMessage("Password must not be empty");

        RuleFor(r => r.ThirdPassword)
            .NotNull().WithMessage("Password must not be null")
            .NotEmpty().WithMessage("Password must not be empty");
    }
}