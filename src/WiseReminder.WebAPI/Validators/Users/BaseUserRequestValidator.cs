namespace WiseReminder.WebAPI.Validators.Users;

public sealed class BaseUserRequestValidator : AbstractValidator<UserRequest>
{
    public BaseUserRequestValidator()
    {
        RuleFor(r => r.Username)
            .NotNull().WithMessage("Username must not be null")
            .NotEmpty().WithMessage("Username must not be empty");

        RuleFor(r => r.Login)
            .NotNull().WithMessage("Login must not be null")
            .NotEmpty().WithMessage("Login must not be empty");

        RuleFor(r => r.Password)
            .NotNull().WithMessage("Password must not be null")
            .NotEmpty().WithMessage("Password must not be empty");
    }
}