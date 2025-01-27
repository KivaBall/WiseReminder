namespace WiseReminder.Domain.UnitTests.Data;

public static class UserData
{
    public static Username Username => new Username("Username");

    public static Username NewUsername => new Username("NewUsername");

    public static Login Login => new Login("Login");

    public static HashedPassword HashedPassword => new HashedPassword("HashedPassword");

    public static HashedPassword NewHashedPassword => new HashedPassword("NewHashedPassword");

    public static Subscription FreeSubscription => Subscription.Free;

    public static Subscription IronSubscription => Subscription.Iron;

    public static User User => new User(Username, Login, HashedPassword);
}