namespace WiseReminder.Application.Users.Commands.DeleteUser;

public sealed class DeleteUserHandler(
    IUserRepository repository,
    IUnitOfWork unitOfWork,
    IMediator mediator)
    : ICommandHandler<DeleteUserCommand>
{
    public async Task<Result> Handle(
        DeleteUserCommand request,
        CancellationToken cancellationToken)
    {
        var query = new GetUserByIdQuery(request.Id);

        var user = await mediator.Send(query, cancellationToken);

        if (user.IsFailed)
        {
            return UserErrors.UserNotFound;
        }

        repository.DeleteUser(user.Value);

        var result = await unitOfWork.SaveChangesAsync(cancellationToken);

        if (result.IsSuccess)
        {
            var userDeletedEvent = new UserDeletedEvent(request.Id);

            await mediator.Publish(userDeletedEvent, cancellationToken);
        }

        return result;
    }
}