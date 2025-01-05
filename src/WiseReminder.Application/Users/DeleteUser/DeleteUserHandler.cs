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

        var authorQuery = new GetAuthorByUserIdQuery(request.Id);

        var author = await sender.Send(authorQuery, cancellationToken);

        if (author.IsSuccess)
        {
            var authorCommand = new AdminDeleteAuthorCommand(author.Value.Id);

            await sender.Send(authorCommand, cancellationToken);
        }

        userRepository.DeleteUser(user.Value);

        return await unitOfWork.SaveChangesAsync();
    }
}