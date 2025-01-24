namespace WiseReminder.IntegrationTests.Controllers.Users;

public sealed class UsersControllerPutMethodsTests : BaseControllerTests
{
    [Fact]
    public async Task ApplySubscription_WhenAllOk_ReturnsOk()
    {
        //Arrange
        var request = UserData.ApplySubscriptionRequest;

        //Act
        await Client.AdminLoginAsync();

        var preGetResponse = await Client.GetAsync($"api/users/{AdminIds.UserId}");

        var preUserDto = await preGetResponse.ReadJson<UserDto>();

        var applySubscriptionResponse =
            await Client.PutAsync($"api/users/{AdminIds.UserId}/apply-subscription",
                request);

        var postGetResponse = await Client.GetAsync($"api/users/{AdminIds.UserId}");

        var postUserDto = await postGetResponse.ReadJson<UserDto>();

        //Assert
        preGetResponse.StatusCode.Should().Be(HttpStatusCode.OK);

        preUserDto!.Id.Should().Be(AdminIds.UserId);
        preUserDto.Username.Should().Be(UserData.Username);
        preUserDto.Login.Should().Be(UserData.EmptyUserLogin);
        preUserDto.Subscription.Should().Be("Free");

        applySubscriptionResponse.StatusCode.Should().Be(HttpStatusCode.OK);

        postGetResponse.StatusCode.Should().Be(HttpStatusCode.OK);

        postUserDto!.Id.Should().Be(AdminIds.UserId);
        postUserDto.Username.Should().Be(UserData.Username);
        postUserDto.Login.Should().Be(UserData.EmptyUserLogin);
        postUserDto.Subscription.Should().Be("Iron");
    }

    [Fact]
    public async Task ApplySubscription_WhenUser_ReturnsForbidden()
    {
        //Arrange
        var request = UserData.ApplySubscriptionRequest;

        //Act
        await Client.EmptyUserLoginAsync();

        var applySubscriptionResponse =
            await Client.PutAsync($"api/users/{AdminIds.UserId}/apply-subscription",
                request);

        //Assert
        applySubscriptionResponse.StatusCode.Should().Be(HttpStatusCode.Forbidden);
    }

    [Fact]
    public async Task ApplySubscription_WhenUnauthorized_ReturnsUnauthorized()
    {
        //Arrange
        var request = UserData.ApplySubscriptionRequest;

        //Act
        var applySubscriptionResponse =
            await Client.PutAsync($"api/users/{AdminIds.UserId}/apply-subscription",
                request);

        //Assert
        applySubscriptionResponse.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
    }

    [Fact]
    public async Task ApplySubscription_WhenInvalid_ReturnsBadRequest()
    {
        //Arrange
        var request = UserData.InvalidApplySubscriptionRequest;

        //Act
        await Client.AdminLoginAsync();

        var applySubscriptionResponse =
            await Client.PutAsync($"api/users/{AdminIds.UserId}/apply-subscription",
                request);

        //Assert
        applySubscriptionResponse.StatusCode.Should().Be(HttpStatusCode.BadRequest);
    }

    [Fact]
    public async Task UpdateUser_WhenAllOk_ReturnsOk()
    {
        //Arrange
        var request = UserData.UpdateUserRequest();

        //Act
        await Client.EmptyUserLoginAsync();

        var preGetResponse = await Client.GetAsync("api/users/own");

        var preUserDto = await preGetResponse.ReadJson<UserDto>();

        var changeUsernameResponse = await Client.PutAsync("api/users/own", request);

        var postGetResponse = await Client.GetAsync("api/users/own");

        var postUserDto = await postGetResponse.ReadJson<UserDto>();

        //Assert
        preGetResponse.StatusCode.Should().Be(HttpStatusCode.OK);

        preUserDto!.Id.Should().Be(AdminIds.UserId);
        preUserDto.Username.Should().Be(UserData.Username);
        preUserDto.Login.Should().Be(UserData.EmptyUserLogin);

        changeUsernameResponse.StatusCode.Should().Be(HttpStatusCode.OK);

        postGetResponse.StatusCode.Should().Be(HttpStatusCode.OK);

        postUserDto!.Id.Should().Be(AdminIds.UserId);
        postUserDto.Username.Should().Be(UserData.NewUsername);
        postUserDto.Login.Should().Be(UserData.EmptyUserLogin);
    }

    [Fact]
    public async Task UpdateUser_WhenAdmin_ReturnsForbidden()
    {
        //Arrange
        var request = UserData.UpdateUserRequest();

        //Act
        await Client.AdminLoginAsync();

        var response = await Client.PutAsync("api/users/own", request);

        //Assert
        response.StatusCode.Should().Be(HttpStatusCode.Forbidden);
    }

    [Fact]
    public async Task UpdateUser_WhenUnauthorized_ReturnsUnauthorized()
    {
        //Arrange
        var request = UserData.UpdateUserRequest();

        //Act
        var response = await Client.PutAsync("api/users/own", request);

        //Assert
        response.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
    }

    [Fact]
    public async Task UpdateUser_WhenInvalid_ReturnsBadRequest()
    {
        //Arrange
        var request = UserData.InvalidUpdateUserRequest;

        //Act
        await Client.EmptyUserLoginAsync();

        var response = await Client.PutAsync("api/users/own", request);

        //Assert
        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
    }
}