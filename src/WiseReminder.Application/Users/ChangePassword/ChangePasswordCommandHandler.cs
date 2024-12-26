namespace WiseReminder.Application.Users.ChangePassword;

public sealed class ChangePasswordCommandHandler(
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
        var query = new GetUserByIdQuery { Id = request.Id };

        var user = await sender.Send(query);

        if (user.IsFailed)
        {
            return Result.Fail(UserErrors.UserNotFound);
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

        var isSaved = await unitOfWork.SaveChangesAsync();

        return isSaved.IsFailed ? Result.Fail(isSaved.Errors) : Result.Ok();
    }
}