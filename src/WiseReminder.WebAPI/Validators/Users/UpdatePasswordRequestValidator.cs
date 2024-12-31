namespace WiseReminder.WebAPI.Validators.Users;

public sealed class UpdatePasswordRequestValidator : AbstractValidator<ChangePasswordRequest>
{
    public UpdatePasswordRequestValidator()
    {
        RuleFor(r => r.OldPassword)
            .NotNull().WithMessage("Old password must not be null")
            .NotEmpty().WithMessage("Old password must not be empty");

        RuleFor(r => r.NewPassword)
            .NotNull().WithMessage("New password must not be null")
            .NotEmpty().WithMessage("New password must not be empty");
    }
}