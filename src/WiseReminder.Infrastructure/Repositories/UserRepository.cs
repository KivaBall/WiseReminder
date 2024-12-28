namespace WiseReminder.Infrastructure.Repositories;

public sealed class UserRepository(
    AppDbContext context)
    : IUserRepository
{
    public void CreateUser(User user)
    {
        context.Add(user);
    }

    public void UpdateUser(User user)
    {
        context.Update(user);
    }

    public void DeleteUser(User user)
    {
        user.Delete();

        context.Update(user);
    }

    public async Task<User?> GetUserById(Guid id)
    {
        return await context.Users
            .FirstOrDefaultAsync(u => u.Id == id);
    }

    public async Task<User?> GetUserByLogin(Login login)
    {
        return await context.Users
            .FirstOrDefaultAsync(u => u.Login == login);
    }
}