namespace WiseReminder.WebAPI.Validators.Users;

public sealed class UpdateUserRequestValidator : AbstractValidator<UpdateUserRequest>
{
    public UpdateUserRequestValidator()
    {
        RuleFor(r => r.OldPassword)
            .NotNull().WithMessage("Old password must not be null")
            .NotEmpty().WithMessage("Old password must not be empty");

        RuleFor(r => r.NewUsername)
            .NotEmpty().WithMessage("New username must not be empty");

        RuleFor(r => r.NewPassword)
            .NotEmpty().WithMessage("New password must not be empty");
    }
}