namespace WiseReminder.Domain.Users;

public sealed class User : Entity<User>
{
    public User(Username username, Login login, HashedPassword hashedPassword)
    {
        Username = username;
        Login = login;
        HashedPassword = hashedPassword;
        Subscription = Subscription.Free;
    }

    public Username Username { get; private set; }
    public Login Login { get; private set; }
    public HashedPassword HashedPassword { get; private set; }
    public Subscription Subscription { get; private set; }

    public void ApplySubscription(Subscription subscription)
    {
        Subscription = subscription;
    }

    public Result Update(Username? username, HashedPassword? hashedPassword)
    {
        if (username == null && hashedPassword == null)
        {
            return UserErrors.NothingUpdated;
        }

        if (username != null)
        {
            Username = username;
        }

        if (hashedPassword != null)
        {
            HashedPassword = hashedPassword;
        }

        return Result.Ok();
    }
}