namespace WiseReminder.Application.Users.ChangePassword;

public sealed class ChangePasswordHandler(
    IUserRepository userRepository,
    ISender sender,
    IEncryptService encryptService,
    IUnitOfWork unitOfWork)
    : ICommandHandler<ChangePasswordCommand>
{
    public async Task<Result> Handle(
        ChangePasswordCommand request,
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
            request.OldPassword);

        if (!isOldPasswordCorrect)
        {
            return Result.Fail(UserErrors.PasswordNotCorrect);
        }

        var newPassword = new HashedPassword(encryptService.Encrypt(request.NewPassword));

        user.Value.ChangePassword(newPassword);

        userRepository.UpdateUser(user.Value);

        return await unitOfWork.SaveChangesAsync();
    }
}