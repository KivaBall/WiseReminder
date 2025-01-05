namespace WiseReminder.IntegrationTests.Controllers.Authors;

public sealed class AuthorsControllerPostMethodsTests : BaseControllerTests
{
    [Fact]
    public async Task AdminCreateAuthor_WhenAllOk_ReturnsOk()
    {
        //Arrange
        var request = AuthorData.CreateAdminAuthorRequest;

        //Act
        await Client.AdminLoginAsync();

        var createResponse = await Client.PostAsync("api/authors", request);

        var id = await createResponse.ReadJson<Guid>();

        var getResponse = await Client.GetAsync($"api/authors/{id}");

        var authorDto = await getResponse.ReadJson<AuthorDto>();

        //Assert
        createResponse.StatusCode.Should().Be(HttpStatusCode.OK);

        getResponse.StatusCode.Should().Be(HttpStatusCode.OK);

        authorDto!.Id.Should().Be(id);
        authorDto.Name.Should().Be(AuthorData.DefaultName);
        authorDto.Biography.Should().Be(AuthorData.DefaultBiography);
        authorDto.BirthDate.Should().Be(AuthorData.DefaultBirthDate);
        authorDto.DeathDate.Should().Be(AuthorData.DefaultDeathDate);
    }

    [Fact]
    public async Task AdminCreateAuthor_WhenUser_ReturnsForbidden()
    {
        //Arrange
        var request = AuthorData.CreateAdminAuthorRequest;

        //Act
        await Client.EmptyUserLoginAsync();

        var response = await Client.PostAsync("api/authors", request);

        //Assert
        response.StatusCode.Should().Be(HttpStatusCode.Forbidden);
    }

    [Fact]
    public async Task AdminCreateAuthor_WhenUnauthorized_ReturnsUnauthorized()
    {
        //Arrange
        var request = AuthorData.CreateAdminAuthorRequest;

        //Act
        var response = await Client.PostAsync("api/authors", request);

        //Assert
        response.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
    }

    [Fact]
    public async Task AdminCreateAuthor_WhenInvalid_ReturnsBadRequest()
    {
        //Arrange
        var request = AuthorData.InvalidAdminAuthorRequest;

        //Act
        await Client.AdminLoginAsync();

        var response = await Client.PostAsync("api/authors", request);

        //Assert
        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
    }

    [Fact]
    public async Task UserCreateAuthor_WhenAllOk_ReturnsOk()
    {
        //Arrange
        var request = AuthorData.CreateUserAuthorRequest;

        //Act
        await Client.EmptyUserLoginAsync();

        var createResponse = await Client.PostAsync("api/authors/own", request);

        var id = await createResponse.ReadJson<Guid>();

        var getResponse = await Client.GetAsync($"api/authors/{id}");

        var authorDto = await getResponse.ReadJson<AuthorDto>();

        //Assert
        createResponse.StatusCode.Should().Be(HttpStatusCode.OK);

        getResponse.StatusCode.Should().Be(HttpStatusCode.OK);

        authorDto!.Id.Should().Be(id);
        authorDto.Name.Should().Be(AuthorData.DefaultName);
        authorDto.Biography.Should().Be(AuthorData.DefaultBiography);
        authorDto.BirthDate.Should().Be(AuthorData.DefaultBirthDate);
        authorDto.DeathDate.Should().BeNull();
        authorDto.UserId.Should().Be(AdminIds.UserId);
    }

    [Fact]
    public async Task UserCreateAuthor_WhenAdmin_ReturnsForbidden()
    {
        //Arrange
        var request = AuthorData.CreateUserAuthorRequest;

        //Act
        await Client.AdminLoginAsync();

        var response = await Client.PostAsync("api/authors/own", request);

        //Assert
        response.StatusCode.Should().Be(HttpStatusCode.Forbidden);
    }

    [Fact]
    public async Task UserCreateAuthor_WhenUnauthorized_ReturnsUnauthorized()
    {
        //Arrange
        var request = AuthorData.CreateUserAuthorRequest;

        //Act
        var response = await Client.PostAsync("api/authors/own", request);

        //Assert
        response.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
    }

    [Fact]
    public async Task UserCreateAuthor_WhenInvalid_ReturnsBadRequest()
    {
        //Arrange
        var request = AuthorData.InvalidUserAuthorRequest;

        //Act
        await Client.EmptyUserLoginAsync();

        var response = await Client.PostAsync("api/authors/own", request);

        //Assert
        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
    }

    [Fact]
    public async Task UserCreateAuthor_WhenAuthorExistsInUser_ReturnsBadRequest()
    {
        //Arrange
        var request = AuthorData.InvalidUserAuthorRequest;

        //Act
        await Client.UserWithDataLoginAsync();

        var response = await Client.PostAsync("api/authors/own", request);

        //Assert
        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
    }
}