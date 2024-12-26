namespace WiseReminder.Application.Users.ChangeUsername;

public sealed class ChangeUsernameCommandHandler(
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
        var query = new GetUserByIdQuery { Id = request.Id };

        var user = await sender.Send(query);

        if (user.IsFailed)
        {
            return Result.Fail(UserErrors.UserNotFound);
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
        
        var isSaved = await unitOfWork.SaveChangesAsync();

        return isSaved.IsFailed ? Result.Fail(isSaved.Errors) : Result.Ok();
    }
}