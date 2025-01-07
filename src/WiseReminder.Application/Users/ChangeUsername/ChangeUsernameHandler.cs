namespace WiseReminder.Application.Users.ChangeUsername;

public sealed class ChangeUsernameHandler(
    IUserRepository repository,
    IEncryptService encryptService,
    ISender sender,
    IUnitOfWork unitOfWork)
    : ICommandHandler<ChangeUsernameCommand>
{
    public async Task<Result> Handle(
        ChangeUsernameCommand request,
        CancellationToken cancellationToken)
    {
        var query = new GetUserByIdQuery(request.Id);

        var user = await sender.Send(query, cancellationToken);

        if (user.IsFailed)
        {
            return user.ToResult();
        }

        var isOldPasswordCorrect = encryptService.Check(
            user.Value.HashedPassword.Value,
            request.Password);

        if (!isOldPasswordCorrect)
        {
            return UserErrors.PasswordNotCorrect;
        }

        var newUsername = new Username(request.NewUsername);

        user.Value.ChangeUsername(newUsername);

        repository.UpdateUser(user.Value);

        return await unitOfWork.SaveChangesAsync();
    }
}