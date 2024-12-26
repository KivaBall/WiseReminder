namespace WiseReminder.Domain.Authors;

public sealed record Biography
{
    public Biography(string value)
    {
        Value = value;
    }

    public string Value { get; }
}