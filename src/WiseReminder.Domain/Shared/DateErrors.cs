namespace WiseReminder.Domain.Shared;

public static class DateErrors
{
    public static readonly Result YearOutOfRange =
        new Error("The year must be between 1 and 2048");
}