namespace WiseReminder.Application.Users.LoginAsAdmin;

public sealed class LoginAsAdminCommandHandler(
    IJwtService jwtService)
    : ICommandHandler<LoginAsAdminCommand, string>
{
    public async Task<Result<string>> Handle(
        LoginAsAdminCommand request,
        CancellationToken cancellationToken)
    {
        if (request.FirstPassword != "first_secret_admin_password")
        {
            return Result.Fail(UserErrors.PasswordNotCorrect);
        }

        if (request.SecondPassword != "second_secret_admin_password")
        {
            return Result.Fail(UserErrors.PasswordNotCorrect);
        }

        if (request.ThirdPassword != "third_secret_admin_password")
        {
            return Result.Fail(UserErrors.PasswordNotCorrect);
        }

        var token = jwtService.GenerateJwtTokenForAdmin();

        return await Task.FromResult(Result.Ok(token));
    }
}