namespace WiseReminder.Domain.Authors;

public sealed record AuthorName
{
    public AuthorName(string value)
    {
        Value = value;
    }

    public string Value { get; }
}