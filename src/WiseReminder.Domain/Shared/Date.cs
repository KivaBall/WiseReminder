namespace WiseReminder.Domain.Shared;

public sealed record Date
{
    private Date(short year, byte month, byte day)
    {
        Year = year;
        Month = month;
        Day = day;
    }

    public short Year { get; }
    public byte Month { get; }
    public byte Day { get; }

    public static Result<Date> Create(short year, byte month, byte day)
    {
        const short minYear = 1;

        if (year < minYear || year > DateTime.Now.Year)
        {
            return Result.Fail("Year must be between 1 and the current one");
        }

        const byte minMonth = 1;
        const byte maxMonth = 12;

        if (month < minMonth || month > maxMonth)
        {
            return Result.Fail("Month must be between 1 and 12");
        }

        const byte minDay = 1;

        if (day < minDay || day > DateTime.DaysInMonth(year, month))
        {
            return Result.Fail("Day must be between 1 and 31");
        }

        var date = new Date(year, month, day);

        return Result.Ok(date);
    }
}