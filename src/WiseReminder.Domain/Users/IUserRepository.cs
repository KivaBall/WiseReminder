namespace WiseReminder.Domain.Users;

public interface IUserRepository
{
    void CreateUser(User user);
    void UpdateUser(User user);
    Task DeleteUser(User user);
    Task<User?> GetUserById(Guid id);
}