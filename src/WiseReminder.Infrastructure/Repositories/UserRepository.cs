namespace WiseReminder.Infrastructure.Repositories;

public sealed class UserRepository(
    AppDbContext context)
    : IUserRepository
{
    public void CreateUser(User user)
    {
        context.Users.Add(user);
    }

    public void UpdateUser(User user)
    {
        context.Users.Update(user);
    }

    public void DeleteUser(User user)
    {
        context.Users.Remove(user);
    }

    public async Task<User?> GetUserById(Guid id, CancellationToken cancellationToken)
    {
        return await context.Users
            .FirstOrDefaultAsync(u => u.Id == id, cancellationToken);
    }

    public async Task<bool> HasUserById(Guid id, CancellationToken cancellationToken)
    {
        return await context.Users
            .AnyAsync(u => u.Id == id, cancellationToken);
    }

    public async Task<User?> GetUserByLogin(Login login, CancellationToken cancellationToken)
    {
        return await context.Users
            .FirstOrDefaultAsync(u => u.Login == login, cancellationToken);
    }
}