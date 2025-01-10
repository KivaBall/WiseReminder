namespace WiseReminder.Domain.Quotes;

public static class QuoteErrors
{
    public static readonly Result QuoteNotFound =
        new Error("The quote with the specified ID was not found");

    public static readonly Result QuoteDateOutOfRange =
        new Error("The quote date is outside the " +
                  "allowed range based on the author's birth and death dates");

    public static readonly Result QuoteLimitExceeded =
        new Error("The author has exceeded the allowed number of quotes for their subscription");

    public static readonly Result QuoteNotBelongsToThisAuthor =
        new Error("The quote is not belong to this author");
}