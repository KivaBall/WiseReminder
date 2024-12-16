namespace WiseReminder.Domain.Authors;

public sealed record AuthorBiography
{
    public AuthorBiography(string value)
    {
        Value = value;
    }

    public string Value { get; }
}