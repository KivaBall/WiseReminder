namespace WiseReminder.Application.Users.DeleteUser;

public sealed class DeleteUserCommandHandler(
    IUserRepository userRepository,
    IUnitOfWork unitOfWork,
    ISender sender)
    : ICommandHandler<DeleteUserCommand>
{
    public async Task<Result> Handle(
        DeleteUserCommand request,
        CancellationToken cancellationToken)
    {
        var query = new GetUserByIdQuery { Id = request.Id };

        var user = await sender.Send(query);

        if (user.IsFailed)
        {
            return Result.Fail(UserErrors.UserNotFound);
        }

        if (user.Value.AuthorId != null)
        {
            var authorQuery = new DeleteAuthorAsAdminCommand { Id = user.Value.AuthorId.Value };

            await sender.Send(authorQuery, cancellationToken);
        }

        userRepository.DeleteUser(user.Value);

        var isSaved = await unitOfWork.SaveChangesAsync();

        return isSaved.IsFailed ? Result.Fail(isSaved.Errors) : Result.Ok();
    }
}