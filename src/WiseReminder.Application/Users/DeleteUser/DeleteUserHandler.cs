namespace WiseReminder.Application.Users.DeleteUser;

public sealed class DeleteUserHandler(
    IUserRepository userRepository,
    IUnitOfWork unitOfWork,
    ISender sender)
    : ICommandHandler<DeleteUserCommand>
{
    public async Task<Result> Handle(
        DeleteUserCommand request,
        CancellationToken cancellationToken)
    {
        var query = new GetUserByIdQuery(request.Id);

        var user = await sender.Send(query, cancellationToken);

        if (user.IsFailed)
        {
            return Result.Fail(UserErrors.UserNotFound);
        }

        if (user.Value.AuthorId != null)
        {
            var authorQuery = new AdminDeleteAuthorCommand(user.Value.AuthorId.Value); //TODO: Is it necessary for db? Checking is needed

            await sender.Send(authorQuery, cancellationToken);
        }

        userRepository.DeleteUser(user.Value);

        return await unitOfWork.SaveChangesAsync();
    }
}