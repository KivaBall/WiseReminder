namespace WiseReminder.Domain.Shared;

public sealed record Date
{
    private Date(DateOnly value)
    {
        Value = value;
    }

    public DateOnly Value { get; }

    public static Result<Date> Create(DateOnly dateStruct)
    {
        const short maxYear = 2048;

        if (dateStruct.Year > maxYear)
        {
            return DateErrors.YearOutOfRange;
        }

        var date = new Date(dateStruct);

        return Result.Ok(date);
    }
}