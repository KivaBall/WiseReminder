namespace WiseReminder.Domain.UnitTests.Entities;

public class AuthorTests
{
    [Theory]
    [InlineData(1860, 7, 20, 1850, 7, 20)]
    [InlineData(1900, 7, 20, 1900, 7, 20)]
    [InlineData(1900, 7, 20, 1900, 2, 24)]
    [InlineData(2021, 7, 20, 52, 7, 20)]
    [InlineData(1800, 7, 20, 1800, 6, 10)]
    public void CreateAuthor_WhenBirthDateAndDeathDateNotValid_ShouldReturnFailure(short birthYear,
        byte birthMonth, byte birthDay, short deathYear, byte deathMonth, byte deathDay)
    {
        // Arrange
        var birthDate = Date.Create(new DateOnly(birthYear, birthMonth, birthDay)).Value;
        var deathDate = Date.Create(new DateOnly(deathYear, deathMonth, deathDay)).Value;

        // Act
        var author = Author.Create(new AuthorName("John Smith"), new Biography(". . ."),
            birthDate, deathDate, null);

        // Assert
        author.IsSuccess.Should().BeFalse();
    }

    [Theory]
    [InlineData(1850, 7, 20, 1860, 7, 20)]
    [InlineData(1900, 7, 20, 1920, 9, 20)]
    [InlineData(1900, 2, 24, 1910, 7, 20)]
    [InlineData(52, 7, 20, 2021, 7, 20)]
    [InlineData(1800, 6, 10, 1880, 7, 20)]
    public void CreateAuthor_WhenBirthDateAndDeathDateValid_ShouldReturnSuccess(short birthYear,
        byte birthMonth, byte birthDay, short deathYear, byte deathMonth, byte deathDay)
    {
        // Arrange
        var birthDate = Date.Create(new DateOnly(birthYear, birthMonth, birthDay)).Value;
        var deathDate = Date.Create(new DateOnly(deathYear, deathMonth, deathDay)).Value;

        // Act
        var author = Author.Create(new AuthorName("John Smith"), new Biography(". . ."),
            birthDate, deathDate, null);

        // Assert
        author.IsSuccess.Should().BeTrue();
    }

    [Theory]
    [InlineData(1850, 7, 20)]
    [InlineData(1900, 7, 20)]
    [InlineData(1900, 2, 24)]
    [InlineData(52, 7, 20)]
    [InlineData(1800, 6, 10)]
    public void CreateAuthor_WhenAuthorNotDeadYet_ShouldReturnSuccess(short birthYear,
        byte birthMonth, byte birthDay)
    {
        // Arrange
        var birthDate = Date.Create(new DateOnly(birthYear, birthMonth, birthDay)).Value;

        // Act
        var author = Author.Create(new AuthorName("John Smith"), new Biography(". . ."),
            birthDate, null, null);

        // Assert
        author.IsSuccess.Should().BeTrue();
    }
}