namespace WiseReminder.Domain.Users;

public interface IUserRepository
{
    void CreateUser(User user);

    void UpdateUser(User user);

    void DeleteUser(User user);

    Task<User?> GetUserById(Guid id, CancellationToken cancellationToken);

    Task<bool> HasUserById(Guid id, CancellationToken cancellationToken);

    Task<User?> GetUserByLogin(Login login, CancellationToken cancellationToken);
}