namespace WiseReminder.Application.Users.Commands.LoginAsAdmin;

public sealed class LoginAsAdminHandler(
    IJwtService jwtService)
    : ICommandHandler<LoginAsAdminCommand, string>
{
    public async Task<Result<string>> Handle(
        LoginAsAdminCommand request,
        CancellationToken cancellationToken)
    {
        if (request.FirstPassword != "first_secret_admin_password")
        {
            return UserErrors.PasswordNotCorrect;
        }

        if (request.SecondPassword != "second_secret_admin_password")
        {
            return UserErrors.PasswordNotCorrect;
        }

        if (request.ThirdPassword != "third_secret_admin_password")
        {
            return UserErrors.PasswordNotCorrect;
        }

        var token = jwtService.GenerateTokenForAdmin();

        return await Task.FromResult(Result.Ok(token));
    }
}