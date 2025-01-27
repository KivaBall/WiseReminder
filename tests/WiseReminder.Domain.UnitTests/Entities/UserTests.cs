namespace WiseReminder.Domain.UnitTests.Entities;

public sealed class UserTests
{
    [Fact]
    public void CreateUser()
    {
        //Arrange
        var username = UserData.Username;

        var login = UserData.Login;

        var hashedPassword = UserData.HashedPassword;

        //Act
        var user = new User(username, login, hashedPassword);

        //Assert
        user.Username.Should().Be(UserData.Username);
        user.Login.Should().Be(UserData.Login);
        user.HashedPassword.Should().Be(UserData.HashedPassword);
        user.Subscription.Should().Be(UserData.FreeSubscription);
    }

    [Fact]
    public void UpdateUser_WhenAllOk_ReturnsSuccess()
    {
        //Arrange
        var user = UserData.User;

        var newUsername = UserData.NewUsername;

        var newHashedPassword = UserData.NewHashedPassword;

        //Act
        var result = user.Update(newUsername, newHashedPassword);

        //Assert
        result.IsSuccess.Should().BeTrue();

        user.Username.Should().Be(UserData.NewUsername);
        user.Login.Should().Be(UserData.Login);
        user.HashedPassword.Should().Be(UserData.NewHashedPassword);
        user.Subscription.Should().Be(UserData.FreeSubscription);
    }

    [Fact]
    public void UpdateUser_WhenNothingUpdated_ReturnsFailure()
    {
        //Arrange
        var user = UserData.User;

        Username? newUsername = null;

        HashedPassword? newHashedPassword = null;

        //Act
        var result = user.Update(newUsername, newHashedPassword);

        //Assert
        result.Equals(UserErrors.NothingUpdated).Should().BeTrue();

        user.Username.Should().Be(UserData.Username);
        user.Login.Should().Be(UserData.Login);
        user.HashedPassword.Should().Be(UserData.HashedPassword);
        user.Subscription.Should().Be(UserData.FreeSubscription);
    }

    [Fact]
    public void ApplySubscription()
    {
        //Arrange
        var user = UserData.User;

        var subscription = UserData.IronSubscription;

        //Act
        user.ApplySubscription(subscription);

        //Assert
        user.Username.Should().Be(UserData.Username);
        user.Login.Should().Be(UserData.Login);
        user.HashedPassword.Should().Be(UserData.HashedPassword);
        user.Subscription.Should().Be(UserData.IronSubscription);
    }
}