namespace WiseReminder.Application.Users.Commands.ApplySubscription;

public sealed class ApplySubscriptionHandler(
    IUserRepository repository,
    IUnitOfWork unitOfWork,
    ISender sender)
    : ICommandHandler<ApplySubscriptionCommand>
{
    public async Task<Result> Handle(ApplySubscriptionCommand request,
        CancellationToken cancellationToken)
    {
        var query = new GetUserByIdQuery(request.UserId);

        var user = await sender.Send(query, cancellationToken);

        if (user.IsFailed)
        {
            return user.ToResult();
        }

        var isSubscription = Enum.TryParse<Subscription>(
            request.Subscription,
            out var subscription);

        if (!isSubscription)
        {
            return UserErrors.IncorrectNameOfSubscription;
        }

        user.Value.ApplySubscription(subscription);

        repository.UpdateUser(user.Value);

        return await unitOfWork.SaveChangesAsync(cancellationToken);
    }
}