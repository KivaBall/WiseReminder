namespace WiseReminder.Domain.Shared;

public static class DateErrors
{
    public static IError YearOutOfRange =>
        new Error("Year must be between 1 and 2048");
}