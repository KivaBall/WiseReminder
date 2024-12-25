namespace WiseReminder.Domain.Users;

public sealed record HashedPassword
{
    public HashedPassword(string value)
    {
        Value = value;
    }
    
    public string Value { get; }
}