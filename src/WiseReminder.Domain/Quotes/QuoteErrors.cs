namespace WiseReminder.Domain.Quotes;

public static class QuoteErrors
{
    public static IError QuoteNotFound =>
        new Error("Quote by Id was not found");

    public static IError QuoteDateOutOfRange =>
        new Error("The date of quote is under bound of needed birth and death dates");
}