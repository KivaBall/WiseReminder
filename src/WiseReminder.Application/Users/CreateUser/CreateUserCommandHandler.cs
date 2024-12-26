namespace WiseReminder.Application.Users.CreateUser;

public sealed class CreateUserCommandHandler(
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

        var password = new HashedPassword(encryptService.Encrypt(request.Password));

        var user = new User(username, login, password);

        userRepository.CreateUser(user);

        var isSaved = await unitOfWork.SaveChangesAsync();

        return isSaved.IsFailed ? Result.Fail(isSaved.Errors) : Result.Ok(user.Id);
    }
}