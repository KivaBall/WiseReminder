namespace WiseReminder.Application.Users.CreateUser;

public sealed class CreateUserHandler(
    IUserRepository userRepository,
    IUnitOfWork unitOfWork,
    IEncryptService encryptService)
    : ICommandHandler<CreateUserCommand, Guid>
{
    public async Task<Result<Guid>> Handle(
        CreateUserCommand request,
        CancellationToken cancellationToken)
    {
        var username = new Username(request.Username);

        var login = new Login(request.Login);

        var user = await userRepository.GetUserByLogin(login);

        if (user != null)
        {
            return Result.Fail(UserErrors.LoginAlreadyExists);
        }

        var password = new HashedPassword(encryptService.Encrypt(request.Password));

        user = new User(username, login, password);

        userRepository.CreateUser(user);

        return await unitOfWork.SaveChangesAsyncWithResult(() => user.Id);
    }
}