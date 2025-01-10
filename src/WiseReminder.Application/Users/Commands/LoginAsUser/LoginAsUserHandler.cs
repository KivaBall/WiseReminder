namespace WiseReminder.Application.Users.Commands.LoginAsUser;

public sealed class LoginAsUserHandler(
    IUserRepository repository,
    IEncryptService encryptService,
    IJwtService jwtService)
    : ICommandHandler<LoginAsUserCommand, string>
{
    public async Task<Result<string>> Handle(
        LoginAsUserCommand request,
        CancellationToken cancellationToken)
    {
        var login = new Login(request.Login);

        var user = await repository.GetUserByLogin(login, cancellationToken);

        if (user == null)
        {
            return UserErrors.UserNotFound;
        }

        var isCorrectPassword = encryptService.Check(user.HashedPassword.Value, request.Password);

        if (!isCorrectPassword)
        {
            return UserErrors.PasswordNotCorrect;
        }

        var token = jwtService.GenerateTokenForUser(user.Id);

        return Result.Ok(token);
    }
}