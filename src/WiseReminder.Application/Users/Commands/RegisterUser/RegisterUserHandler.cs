namespace WiseReminder.Application.Users.Commands.RegisterUser;

public sealed class RegisterUserHandler(
    IUserRepository repository,
    IUnitOfWork unitOfWork,
    IEncryptService encryptService)
    : ICommandHandler<RegisterUserCommand, Guid>
{
    public async Task<Result<Guid>> Handle(
        RegisterUserCommand request,
        CancellationToken cancellationToken)
    {
        var username = new Username(request.Username);

        var login = new Login(request.Login);

        var user = await repository.GetUserByLogin(login, cancellationToken);

        if (user != null)
        {
            return UserErrors.LoginAlreadyExists;
        }

        var password = new HashedPassword(encryptService.Encrypt(request.Password));

        user = new User(username, login, password);

        repository.CreateUser(user);

        return await unitOfWork.SaveChangesAsync(user.Id, cancellationToken);
    }
}