namespace WiseReminder.Domain.UnitTests.ValueObjects;

public class DateTests
{
    [Theory]
    [InlineData(9999)]
    [InlineData(2049)]
    [InlineData(2412)]
    [InlineData(2532)]
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
}