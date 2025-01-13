namespace WiseReminder.Domain.UnitTests.Entities;

public sealed class QuoteTests
{
    [Fact]
    public void CreateByAdmin_WhenAllOk_ReturnsSuccess()
    {
        // Arrange
        var text = QuoteData.Text;

        var quoteDate = QuoteData.QuoteDate;

        // Act
        var result = Quote.CreateByAdmin(text, quoteDate, AuthorData.AdminAuthor, Guid.Empty);

        var quote = result.Value;

        // Assert
        result.IsSuccess.Should().BeTrue();

        quote.Text.Should().Be(QuoteData.Text);
        quote.QuoteDate.Should().Be(QuoteData.QuoteDate);
    }

    [Theory]
    [InlineData("2001")]
    [InlineData("1959")]
    [InlineData("1942")]
    public void CreateByAdmin_WhenQuoteDateOutOfRange_ReturnsFailure(string quoteYear)
    {
        // Arrange
        var text = QuoteData.Text;

        var quoteDate = Date.Create(DateOnly.Parse($"{quoteYear}-01-01")).Value;

        var author = AuthorData.AdminAuthor;

        // Act
        var result = Quote.CreateByAdmin(text, quoteDate, author, Guid.Empty);

        // Assert
        result.Errors.First().Equals(QuoteErrors.QuoteDateOutOfRange.Errors.First()).Should()
            .BeTrue();
    }

    [Fact]
    public void CreateByUser_WhenAllOk_ReturnsSuccess()
    {
        // Arrange
        var text = QuoteData.Text;

        var quoteDate = QuoteData.QuoteDate;

        var author = AuthorData.AdminAuthor;

        // Act
        var result = Quote.CreateByUser(text, author, Guid.Empty, quoteDate,
            Subscription.Free, 4);

        var quote = result.Value;

        // Assert
        result.IsSuccess.Should().BeTrue();

        quote.Text.Should().Be(QuoteData.Text);
        quote.QuoteDate.Should().Be(QuoteData.QuoteDate);
    }

    [Theory]
    [InlineData("1976")]
    [InlineData("1966")]
    [InlineData("2000")]
    public void CreateByUser_WhenQuoteDateOutOfRange_ReturnsFailure(string birthYear)
    {
        // Arrange
        var text = QuoteData.Text;

        var quoteDate = QuoteData.QuoteDate;

        var author = Author.CreateByUser(
            AuthorData.AuthorName,
            AuthorData.Biography,
            Date.Create(DateOnly.Parse($"{birthYear}-01-01")).Value,
            Guid.Empty);

        // Act
        var result = Quote.CreateByUser(text, author, Guid.Empty, quoteDate, Subscription.Free, 4);

        // Assert
        result.Errors.First().Equals(QuoteErrors.QuoteDateOutOfRange.Errors.First()).Should()
            .BeTrue();
    }

    [Fact]
    public void CreateByUser_WhenQuoteLimitExceeded_ReturnsFailure()
    {
        // Arrange
        var text = QuoteData.Text;

        var quoteDate = QuoteData.QuoteDate;

        var author = AuthorData.UserAuthor;

        // Act
        var result = Quote.CreateByUser(text, author, Guid.Empty, quoteDate, Subscription.Free, 5);

        // Assert
        result.Errors.First().Equals(QuoteErrors.QuoteLimitExceeded.Errors.First()).Should()
            .BeTrue();
    }

    [Fact]
    public void CreateByUser_WithInvalidSubscription_ThrowsException()
    {
        // Arrange
        var text = QuoteData.Text;

        var quoteDate = QuoteData.QuoteDate;

        var author = AuthorData.UserAuthor;

        // Act
        Action act = () =>
            Quote.CreateByUser(text, author, Guid.Empty, quoteDate, (Subscription)999, 1);

        // Assert
        act.Should().Throw<ArgumentOutOfRangeException>();
    }

    [Fact]
    public void Update_WhenAllOk_ReturnsSuccess()
    {
        // Arrange
        var quote = QuoteData.AdminQuote;

        var text = QuoteData.NewText;

        var quoteDate = QuoteData.NewQuoteDate;

        // Act
        var result = quote.Update(text, AuthorData.AdminAuthor, Guid.Empty, quoteDate);

        // Assert
        result.IsSuccess.Should().BeTrue();

        quote.Text.Should().Be(QuoteData.NewText);
        quote.QuoteDate.Should().Be(QuoteData.NewQuoteDate);
    }

    [Theory]
    [InlineData("1941")]
    [InlineData("1959")]
    [InlineData("2001")]
    public void Update_WhenQuoteDateOutOfRange_ReturnsFailure(string quoteYear)
    {
        // Arrange
        var quote = QuoteData.AdminQuote;

        var text = QuoteData.NewText;

        var quoteDate = Date.Create(DateOnly.Parse($"{quoteYear}-01-01")).Value;

        // Act
        var result = quote.Update(text, AuthorData.AdminAuthor, Guid.Empty, quoteDate);

        // Assert
        result.Equals(QuoteErrors.QuoteDateOutOfRange).Should().BeTrue();

        quote.Text.Should().Be(QuoteData.Text);
        quote.QuoteDate.Should().Be(QuoteData.QuoteDate);
    }
}