namespace WiseReminder.Domain.UnitTests.Data;

public static class QuoteData
{
    public static Text Text => new Text("Text");

    public static Text NewText => new Text("NewText");

    public static Date QuoteDate => Date.Create(new DateOnly(1975, 1, 1)).Value;

    public static Date NewQuoteDate => Date.Create(new DateOnly(1980, 1, 1)).Value;

    public static Quote AdminQuote => Quote.CreateByAdmin(Text, QuoteDate, AuthorData.AdminAuthor,
        Guid.Empty).Value;
}