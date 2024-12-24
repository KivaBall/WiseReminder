namespace WiseReminder.Domain.Users;

public sealed record Username
{
    public Username(string value)
    {
        Value = value;
    }
    
    public string Value { get; }
}