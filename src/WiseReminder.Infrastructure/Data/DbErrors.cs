namespace WiseReminder.Infrastructure.Data;

public static class DbErrors
{
    public static readonly Result DetectedNoChanges =
        new Error("No changes detected to save something");

    public static readonly Result DatabaseError =
        new Error("Something went wrong");
}