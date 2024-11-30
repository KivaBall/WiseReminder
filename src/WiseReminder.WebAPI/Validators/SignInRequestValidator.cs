using WiseReminder.WebAPI.Controllers.Authorization;

namespace WiseReminder.WebAPI.Validators;

public sealed class SignInRequestValidator : AbstractValidator<SignInRequest>
{
    public SignInRequestValidator()
    {
        RuleFor(l => l.Login)
            .NotNull().WithMessage("Login must not be null")
            .NotEmpty().WithMessage("Login must not be empty");

        RuleFor(l => l.Password)
            .NotNull().WithMessage("Password must not be null")
            .NotEmpty().WithMessage("Password must not be empty");
    }
}