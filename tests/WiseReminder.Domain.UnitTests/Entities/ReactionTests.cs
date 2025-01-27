namespace WiseReminder.Domain.UnitTests.Entities;

public sealed class ReactionTests
{
    [Fact]
    public void CreateReaction()
    {
        //Arrange
        var quoteId = ReactionData.QuoteId;

        var userId = ReactionData.UserId;

        var isLike = ReactionData.IsLike;

        //Act
        var reaction = new Reaction(quoteId, userId, isLike);

        //Assert
        reaction.QuoteId.Should().Be(ReactionData.QuoteId);
        reaction.UserId.Should().Be(ReactionData.UserId);
        reaction.IsLike.Should().Be(ReactionData.IsLike);
    }

    [Fact]
    public void ChangeReaction_WhenAllOk_ReturnsSuccess()
    {
        //Arrange
        var reaction = ReactionData.Reaction;

        var isLike = ReactionData.NewIsLike;

        //Act
        var result = reaction.ChangeReaction(isLike);

        //Assert
        result.IsSuccess.Should().BeTrue();

        reaction.QuoteId.Should().Be(ReactionData.QuoteId);
        reaction.UserId.Should().Be(ReactionData.UserId);
        reaction.IsLike.Should().Be(ReactionData.NewIsLike);
    }

    [Fact]
    public void ChangeReaction_WhenTheSameReaction_ReturnsFailure()
    {
        //Arrange
        var reaction = ReactionData.Reaction;

        var isLike = ReactionData.IsLike;

        //Act
        var result = reaction.ChangeReaction(isLike);

        //Assert
        result.Equals(ReactionErrors.TheSameReaction).Should().BeTrue();

        reaction.QuoteId.Should().Be(ReactionData.QuoteId);
        reaction.UserId.Should().Be(ReactionData.UserId);
        reaction.IsLike.Should().Be(ReactionData.IsLike);
    }
}