namespace WiseReminder.WebAPI.Validators.Users;

public sealed class LoginAsUserRequestValidator : AbstractValidator<LoginAsUserRequest>
{
    public LoginAsUserRequestValidator()
    {
        RuleFor(r => r.Login)
            .NotNull().WithMessage("Login must not be null")
            .NotEmpty().WithMessage("Login must not be empty");

        RuleFor(r => r.Password)
            .NotNull().WithMessage("Password must not be null")
            .NotEmpty().WithMessage("Password must not be empty");
    }
}