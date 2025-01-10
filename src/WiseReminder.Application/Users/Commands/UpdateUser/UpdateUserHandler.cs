namespace WiseReminder.Application.Users.Commands.UpdateUser;

public sealed class UpdateUserHandler(
    IUserRepository repository,
    IEncryptService encryptService,
    ISender sender,
    IUnitOfWork unitOfWork)
    : ICommandHandler<UpdateUserCommand>
{
    public async Task<Result> Handle(
        UpdateUserCommand request,
        CancellationToken cancellationToken)
    {
        var query = new GetUserByIdQuery(request.Id);

        var user = await sender.Send(query, cancellationToken);

        if (user.IsFailed)
        {
            return user.ToResult();
        }

        var isPasswordCorrect = encryptService.Check(
            user.Value.HashedPassword.Value,
            request.OldPassword);

        if (!isPasswordCorrect)
        {
            return UserErrors.PasswordNotCorrect;
        }

        var newUsername = request.NewUsername != null
            ? new Username(request.NewUsername)
            : null;

        var newPassword = request.NewPassword != null
            ? new HashedPassword(encryptService.Encrypt(request.NewPassword))
            : null;

        var result = user.Value.Update(newUsername, newPassword);

        if (result.IsFailed)
        {
            return result;
        }

        repository.UpdateUser(user.Value);

        return await unitOfWork.SaveChangesAsync(cancellationToken);
    }
}