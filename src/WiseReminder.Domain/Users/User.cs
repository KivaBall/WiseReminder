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

    // ReSharper disable once UnusedMember.Local
    private User()
    {
    }

    public Username Username { get; private set; }
    public Login Login { get; private set; }
    public HashedPassword HashedPassword { get; private set; }
    public Subscription Subscription { get; private set; }

    public Author? Author { get; private set; }

    public Result<User> ApplySubscription(Subscription subscription)
    {
        if (Subscription != Subscription.Free)
        {
            return Result.Fail("Subscription has been already applied");
        }

        Subscription = subscription;

        return Result.Ok(this);
    }

    public User ChangeUsername(Username username)
    {
        Username = username;

        return this;
    }

    public User ChangePassword(HashedPassword hashedPassword)
    {
        HashedPassword = hashedPassword;

        return this;
    }
}