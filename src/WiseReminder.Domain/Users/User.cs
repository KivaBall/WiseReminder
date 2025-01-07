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

    public void ChangeUsername(Username username)
    {
        Username = username;
    }

    public void ChangePassword(HashedPassword hashedPassword)
    {
        HashedPassword = hashedPassword;
    }
}