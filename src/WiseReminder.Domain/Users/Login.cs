namespace WiseReminder.Domain.Users;

public sealed record Login
{
    public Login(string value)
    {
        Value = value;
    }

    public string Value { get; }
}