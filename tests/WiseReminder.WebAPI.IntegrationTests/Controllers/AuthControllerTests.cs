namespace WiseReminder.IntegrationTests.Controllers;

public sealed class AuthControllerTests : GenericControllerTests
{
    [Fact]
    public async Task Login_WhenAllOk_ReturnsOk()
    {
        //Arrange
        var request = new SignInRequest
        {
            Login = "wise-reminder-admin-login",
            Password = "wise-reminder-admin-password"
        };

        //Act
        var response = await Client.PostAsJsonAsync("api/auth/login-as-admin", request);

        //Assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);
    }

    [Fact]
    public async Task Login_WhenInvalidData_ReturnsBadRequest()
    {
        //Arrange
        var request = new SignInRequest
        {
            Login = "not-correct-login",
            Password = "not-correct-password"
        };

        //Act
        var response = await Client.PostAsJsonAsync("api/auth/login-as-admin", request);

        //Assert
        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
    }
}