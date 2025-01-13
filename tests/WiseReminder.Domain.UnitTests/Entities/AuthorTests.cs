namespace WiseReminder.Domain.UnitTests.Entities;

public sealed class AuthorTests
{
    [Fact]
    public void CreateAuthorByAdmin_WhenAllOk_ReturnsSuccess()
    {
        // Arrange
        var name = AuthorData.AuthorName;

        var biography = AuthorData.Biography;

        var birthDate = AuthorData.BirthDate;

        var deathDate = AuthorData.DeathDate;

        // Act
        var result = Author.CreateByAdmin(name, biography, birthDate, deathDate);

        var author = result.Value;

        // Assert
        result.IsSuccess.Should().BeTrue();

        author.Name.Should().Be(AuthorData.AuthorName);
        author.Biography.Should().Be(AuthorData.Biography);
        author.BirthDate.Should().Be(AuthorData.BirthDate);
        author.DeathDate.Should().Be(AuthorData.DeathDate);
        author.UserId.Should().BeNull();
    }

    [Theory]
    [InlineData("1860", "1850")]
    [InlineData("1900", "1900")]
    [InlineData("2021", "0052")]
    public void CreateAuthorByAdmin_WhenInvalidBirthAndDeathDateRange_ReturnsFailure(
        string birthYear, string deathYear)
    {
        // Arrange
        var name = AuthorData.AuthorName;

        var biography = AuthorData.Biography;

        var birthDate = Date.Create(DateOnly.Parse($"{birthYear}-01-01")).Value;

        var deathDate = Date.Create(DateOnly.Parse($"{deathYear}-01-01")).Value;

        // Act
        var result = Author.CreateByAdmin(name, biography, birthDate, deathDate);

        // Assert
        result.IsSuccess.Should().BeFalse();
    }

    [Fact]
    public void CreateAuthorByUser()
    {
        // Arrange
        var name = AuthorData.AuthorName;

        var biography = AuthorData.Biography;

        var birthDate = AuthorData.BirthDate;

        var userId = AuthorData.UserId;

        // Act
        var author = Author.CreateByUser(name, biography, birthDate, userId);

        // Assert
        author.Name.Should().Be(AuthorData.AuthorName);
        author.Biography.Should().Be(AuthorData.Biography);
        author.BirthDate.Should().Be(AuthorData.BirthDate);
        author.DeathDate.Should().BeNull();
        author.UserId.Should().Be(AuthorData.UserId);
    }

    [Fact]
    public void UpdateAuthorByAdmin_WhenAllOk_ReturnsSuccess()
    {
        // Arrange
        var author = AuthorData.AdminAuthor;

        var name = AuthorData.NewAuthorName;

        var biography = AuthorData.NewBiography;

        var birthDate = AuthorData.NewBirthDate;

        var deathDate = AuthorData.NewDeathDate;

        var minQuoteDate = Date.Create(DateOnly.Parse("1980-01-01")).Value;

        var maxQuoteDate = Date.Create(DateOnly.Parse("1990-01-01")).Value;

        // Act
        var result = author.UpdateByAdmin(name, biography, birthDate, deathDate, minQuoteDate,
            maxQuoteDate);

        // Assert
        result.IsSuccess.Should().BeTrue();

        author.Name.Should().Be(AuthorData.NewAuthorName);
        author.Biography.Should().Be(AuthorData.NewBiography);
        author.BirthDate.Should().Be(AuthorData.NewBirthDate);
        author.DeathDate.Should().Be(AuthorData.NewDeathDate);
        author.UserId.Should().BeNull();
    }

    [Theory]
    [InlineData("1860", "1850")]
    [InlineData("1950", "1945")]
    [InlineData("2020", "2010")]
    public void UpdateAuthorByAdmin_WhenInvalidBirthAndDeathDateRange_ReturnsFailure(
        string birthYear, string deathYear)
    {
        // Arrange
        var author = AuthorData.AdminAuthor;

        var name = AuthorData.NewAuthorName;

        var biography = AuthorData.NewBiography;

        var birthDate = Date.Create(DateOnly.Parse($"{birthYear}-01-01")).Value;

        var deathDate = Date.Create(DateOnly.Parse($"{deathYear}-01-01")).Value;

        var minQuoteDate = Date.Create(DateOnly.Parse("1970-01-01")).Value;

        var maxQuoteDate = Date.Create(DateOnly.Parse("1980-01-01")).Value;

        // Act
        var result = author.UpdateByAdmin(name, biography, birthDate, deathDate, minQuoteDate,
            maxQuoteDate);

        // Assert
        result.Equals(AuthorErrors.InvalidBirthAndDeathDateRange).Should().BeTrue();

        author.Name.Should().Be(AuthorData.AuthorName);
        author.Biography.Should().Be(AuthorData.Biography);
        author.BirthDate.Should().Be(AuthorData.BirthDate);
        author.DeathDate.Should().Be(AuthorData.DeathDate);
        author.UserId.Should().BeNull();
    }

    [Theory]
    [InlineData("1974")]
    [InlineData("1880")]
    [InlineData("1965")]
    public void UpdateAuthorByAdmin_WhenInvalidBirthAndMinQuoteDateRange_ReturnsFailure(
        string minQuoteYear)
    {
        // Arrange
        var author = AuthorData.AdminAuthor;

        var name = AuthorData.NewAuthorName;

        var biography = AuthorData.NewBiography;

        var birthDate = AuthorData.NewBirthDate;

        var deathDate = AuthorData.NewDeathDate;

        var minQuoteDate = Date.Create(DateOnly.Parse($"{minQuoteYear}-01-01")).Value;

        var maxQuoteDate = Date.Create(DateOnly.Parse("1980-01-01")).Value;

        // Act
        var result = author.UpdateByAdmin(name, biography, birthDate, deathDate, minQuoteDate,
            maxQuoteDate);

        // Assert
        result.Equals(AuthorErrors.InvalidBirthAndMinQuoteDateRange).Should().BeTrue();

        author.Name.Should().Be(AuthorData.AuthorName);
        author.Biography.Should().Be(AuthorData.Biography);
        author.BirthDate.Should().Be(AuthorData.BirthDate);
        author.DeathDate.Should().Be(AuthorData.DeathDate);
        author.UserId.Should().BeNull();
    }

    [Theory]
    [InlineData("1996")]
    [InlineData("2000")]
    [InlineData("2015")]
    public void UpdateAuthorByAdmin_WhenInvalidDeathAndMaxQuoteDateRange_ReturnsFailure(
        string maxQuoteYear)
    {
        // Arrange
        var author = AuthorData.AdminAuthor;

        var name = AuthorData.NewAuthorName;

        var biography = AuthorData.NewBiography;

        var birthDate = AuthorData.NewBirthDate;

        var deathDate = AuthorData.NewDeathDate;

        var minQuoteDate = Date.Create(DateOnly.Parse("1980-01-01")).Value;

        var maxQuoteDate = Date.Create(DateOnly.Parse($"{maxQuoteYear}-01-01")).Value;

        // Act
        var result = author.UpdateByAdmin(name, biography, birthDate, deathDate, minQuoteDate,
            maxQuoteDate);

        // Assert
        result.Equals(AuthorErrors.InvalidDeathAndMaxQuoteDateRange).Should().BeTrue();

        author.Name.Should().Be(AuthorData.AuthorName);
        author.Biography.Should().Be(AuthorData.Biography);
        author.BirthDate.Should().Be(AuthorData.BirthDate);
        author.DeathDate.Should().Be(AuthorData.DeathDate);
        author.UserId.Should().BeNull();
    }

    [Fact]
    public void UpdateAuthorByUser_WhenAllOk_ReturnsSuccess()
    {
        // Arrange
        var author = AuthorData.UserAuthor;

        var name = AuthorData.NewAuthorName;

        var biography = AuthorData.NewBiography;

        var birthDate = AuthorData.NewBirthDate;

        var minQuoteDate = Date.Create(DateOnly.Parse("1976-01-01")).Value;

        // Act
        var result = author.UpdateByUser(name, biography, birthDate, minQuoteDate);

        // Assert
        result.IsSuccess.Should().BeTrue();

        author.Name.Should().Be(AuthorData.NewAuthorName);
        author.Biography.Should().Be(AuthorData.NewBiography);
        author.BirthDate.Should().Be(AuthorData.NewBirthDate);
        author.DeathDate.Should().BeNull();
        author.UserId.Should().Be(AuthorData.UserId);
    }

    [Theory]
    [InlineData("1855")]
    [InlineData("1940")]
    [InlineData("1974")]
    public void UpdateAuthorByUser_WhenInvalidBirthAndMinQuoteDateRange_ReturnsFailure(
        string minQuoteYear)
    {
        // Arrange
        var author = AuthorData.UserAuthor;

        var name = AuthorData.NewAuthorName;

        var biography = AuthorData.NewBiography;

        var birthDate = AuthorData.NewBirthDate;

        var minQuoteDate = Date.Create(DateOnly.Parse($"{minQuoteYear}-01-01")).Value;

        // Act
        var result = author.UpdateByUser(name, biography, birthDate, minQuoteDate);

        // Assert
        result.Equals(AuthorErrors.InvalidBirthAndMinQuoteDateRange).Should().BeTrue();

        author.Name.Should().Be(AuthorData.AuthorName);
        author.Biography.Should().Be(AuthorData.Biography);
        author.BirthDate.Should().Be(AuthorData.BirthDate);
        author.DeathDate.Should().BeNull();
        author.UserId.Should().Be(AuthorData.UserId);
    }
}