namespace WiseReminder.Application.Users.LoginAsUser;

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

        var user = await repository.GetUserByLogin(login);

        if (user == null)
        {
            return UserErrors.UserNotFound;
        }

        var isCorrectPassword = encryptService.Check(user.HashedPassword.Value, request.Password);

        if (!isCorrectPassword)
        {
            return UserErrors.PasswordNotCorrect;
        }

        var token = jwtService.GenerateJwtTokenForUser(user.Id);

        return Result.Ok(token);
    }
}