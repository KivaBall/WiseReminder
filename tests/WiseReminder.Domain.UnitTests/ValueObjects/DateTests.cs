namespace WiseReminder.Domain.UnitTests.ValueObjects;

public class DateTests
{
    [Theory]
    [InlineData(0)]
    [InlineData(2049)]
    [InlineData(-2412)]
    [InlineData(-25)]
    [InlineData(2504)]
    public void CreateDate_WhenYearOutOfRange_ShouldReturnFailure(short year)
    {
        // Act
        var date = Date.Create(new DateOnly(year, 6, 6));

        // Assert
        date.IsSuccess.Should().BeFalse();
    }

    [Theory]
    [InlineData(265)]
    [InlineData(2021)]
    [InlineData(1)]
    [InlineData(832)]
    [InlineData(2048)]
    public void CreateDate_WhenYearInRange_ShouldReturnSuccess(short year)
    {
        // Act
        var date = Date.Create(new DateOnly(year, 6, 6));

        // Assert
        date.IsSuccess.Should().BeTrue();
    }

    [Theory]
    [InlineData(0)]
    [InlineData(13)]
    [InlineData(35)]
    [InlineData(24)]
    [InlineData(14)]
    public void CreateDate_WhenMonthOutOfRange_ShouldReturnFailure(byte month)
    {
        // Act
        var date = Date.Create(new DateOnly(2020, month, 6));

        // Assert
        date.IsSuccess.Should().BeFalse();
    }

    [Theory]
    [InlineData(1)]
    [InlineData(12)]
    [InlineData(7)]
    [InlineData(4)]
    [InlineData(3)]
    public void CreateDate_WhenMonthInRange_ShouldReturnSuccess(byte month)
    {
        // Act
        var date = Date.Create(new DateOnly(2020, month, 6));

        // Assert
        date.IsSuccess.Should().BeTrue();
    }

    [Theory]
    [InlineData(2021, 2, 29)]
    [InlineData(2020, 2, 30)]
    [InlineData(2019, 4, 31)]
    [InlineData(2018, 13, 1)]
    [InlineData(2017, 0, 15)]
    public void CreateDate_WhenDayOutOfRange_ShouldReturnFailure(short year, byte month, byte day)
    {
        // Act
        var date = Date.Create(new DateOnly(year, month, day));

        // Assert
        date.IsSuccess.Should().BeFalse();
    }

    [Theory]
    [InlineData(2020, 2, 29)]
    [InlineData(2016, 2, 29)]
    [InlineData(2019, 3, 31)]
    [InlineData(2017, 12, 25)]
    [InlineData(2000, 1, 1)]
    public void CreateDate_WhenDayInRange_ShouldReturnSuccess(short year, byte month, byte day)
    {
        // Act
        var date = Date.Create(new DateOnly(year, month, day));

        // Assert
        date.IsSuccess.Should().BeTrue();
    }
}