namespace WiseReminder.Application.Users.ChangeUsername;

public sealed class ChangeUsernameHandler(
    IUserRepository userRepository,
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
            return Result.Fail(UserErrors.PasswordNotCorrect);
        }

        var newUsername = new Username(request.NewUsername);

        user.Value.ChangeUsername(newUsername);

        userRepository.UpdateUser(user.Value);

        return await unitOfWork.SaveChangesAsync();
    }
}