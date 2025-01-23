namespace WiseReminder.Domain.UnitTests.ValueObjects;

public sealed class DateTests
{
    [Theory]
    [InlineData(265)]
    [InlineData(2021)]
    [InlineData(1)]
    public void CreateDate_WhenAllOk_ReturnsSuccess(short year)
    {
        // Act
        var date = Date.Create(new DateOnly(year, 6, 6));

        // Assert
        date.IsSuccess.Should().BeTrue();
    }

    [Theory]
    [InlineData(9999)]
    [InlineData(2049)]
    [InlineData(2504)]
    public void CreateDate_WhenYearOutOfRange_ReturnsFailure(short year)
    {
        // Act
        var date = Date.Create(new DateOnly(year, 6, 6));

        // Assert
        date.IsSuccess.Should().BeFalse();
    }
}