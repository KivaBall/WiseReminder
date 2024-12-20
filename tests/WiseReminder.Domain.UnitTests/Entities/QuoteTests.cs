namespace WiseReminder.Domain.UnitTests.Entities;

public class QuoteTests
{
    [Theory]
    [InlineData(1880, 1920, 1885)]
    [InlineData(1880, 1920, 1880)]
    [InlineData(1880, 1920, 1780)]
    [InlineData(1880, 1920, 1889)]
    [InlineData(1880, 1920, 1920)]
    public void CreateQuote_WhenQuoteDateOutOfRange_ShouldReturnFailure(short birthYear,
        short deathYear, short quoteYear)
    {
        // Arrange
        var category = new Category(
            new CategoryName("TestName"),
            new CategoryDescription("TestDescription"));

        var author = Author.Create(
            new AuthorName("TestName"),
            new AuthorBiography("TestBiography"),
            Date.Create(birthYear, 1, 1).Value,
            Date.Create(deathYear, 1, 1).Value).Value;

        // Act
        var quote = Quote.Create(
            new QuoteText("TestText"),
            author,
            category,
            Date.Create(quoteYear, 1, 1).Value);

        // Assert
        quote.IsSuccess.Should().BeFalse();
    }

    [Theory]
    [InlineData(1880, 1920, 1890)]
    [InlineData(1880, 1920, 1900)]
    [InlineData(1880, 1920, 1910)]
    [InlineData(1880, 1920, 1915)]
    [InlineData(1880, 1920, 1919)]
    public void CreateQuote_WhenQuoteDateInRange_ShouldReturnSuccess(short birthYear,
        short deathYear, short quoteYear)
    {
        // Arrange
        var category = new Category(
            new CategoryName("TestName"),
            new CategoryDescription("TestDescription"));

        var author = Author.Create(
            new AuthorName("TestName"),
            new AuthorBiography("TestBiography"),
            Date.Create(birthYear, 1, 1).Value,
            Date.Create(deathYear, 1, 1).Value).Value;

        // Act
        var quote = Quote.Create(
            new QuoteText("TestText"),
            author,
            category,
            Date.Create(quoteYear, 1, 1).Value);

        // Assert
        quote.IsSuccess.Should().BeTrue();
    }
}