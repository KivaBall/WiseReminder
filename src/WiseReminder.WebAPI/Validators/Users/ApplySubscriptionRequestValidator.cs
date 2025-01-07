namespace WiseReminder.WebAPI.Validators.Users;

public sealed class ApplySubscriptionRequestValidator : AbstractValidator<ApplySubscriptionRequest>
{
    public ApplySubscriptionRequestValidator()
    {
        RuleFor(r => r.Subscription)
            .NotEmpty().WithMessage("Subscription must not be empty");
    }
}