namespace WiseReminder.Domain.Users;

public sealed class User : Entity<User>
{
    public User(Username username, Login login, HashedPassword hashedPassword,
        SubscriptionType subscriptionType, Author? author)
    {
        Username = username;
        Login = login;
        HashedPassword = hashedPassword;
        SubscriptionType = subscriptionType;
        AuthorId = author?.Id;
        Author = author;
    }

    // ReSharper disable once UnusedMember.Local
    private User()
    {
    }

    public Username Username { get; private set; }
    public Login Login { get; private set; }
    public HashedPassword HashedPassword { get; private set; }
    public SubscriptionType SubscriptionType { get; private set; }
    public Guid? AuthorId { get; private set; }
    public Author? Author { get; private set; }

    public Result<User> ApplySubscription(SubscriptionType subscriptionType)
    {
        if (SubscriptionType != SubscriptionType.Free)
        {
            return Result.Fail("Subscription has been already applied");
        }

        SubscriptionType = subscriptionType;

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