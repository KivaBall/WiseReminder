namespace WiseReminder.IntegrationTests.Controllers.Authors;

public sealed class AuthorsControllerPutMethodsTests : BaseControllerTests
{
    [Fact]
    public async Task AdminUpdateAuthor_WhenAllOk_ReturnsOk()
    {
        //Arrange
        var request = AuthorData.UpdateAuthorByAdminRequest;

        //Act
        await Client.AdminLoginAsync();

        var preGetResponse = await Client.GetAsync($"api/authors/{AdminIds.AuthorId}");

        var preAuthorDto = await preGetResponse.ReadJson<AuthorDto>();

        var updateResponse = await Client.PutAsync($"api/authors/{AdminIds.AuthorId}", request);

        var postGetResponse = await Client.GetAsync($"api/authors/{AdminIds.AuthorId}");

        var postAuthorDto = await postGetResponse.ReadJson<AuthorDto>();

        //Assert
        preGetResponse.StatusCode.Should().Be(HttpStatusCode.OK);

        preAuthorDto!.Id.Should().Be(AdminIds.AuthorId);
        preAuthorDto.Name.Should().Be(AuthorData.Name);
        preAuthorDto.Biography.Should().Be(AuthorData.Biography);
        preAuthorDto.BirthDate.Should().Be(AuthorData.BirthDate);
        preAuthorDto.DeathDate.Should().Be(AuthorData.DeathDate);

        updateResponse.StatusCode.Should().Be(HttpStatusCode.OK);

        postGetResponse.StatusCode.Should().Be(HttpStatusCode.OK);

        postAuthorDto!.Id.Should().Be(AdminIds.AuthorId);
        postAuthorDto.Name.Should().Be(AuthorData.NewName);
        postAuthorDto.Biography.Should().Be(AuthorData.NewBiography);
        postAuthorDto.BirthDate.Should().Be(AuthorData.NewBirthDate);
        postAuthorDto.DeathDate.Should().Be(AuthorData.NewDeathDate);
    }

    [Fact]
    public async Task AdminUpdateAuthor_WhenUser_ReturnsForbidden()
    {
        //Arrange
        var request = AuthorData.UpdateAuthorByAdminRequest;

        //Act
        await Client.EmptyUserLoginAsync();

        var response = await Client.PutAsync($"api/authors/{AdminIds.AuthorId}", request);

        //Assert
        response.StatusCode.Should().Be(HttpStatusCode.Forbidden);
    }

    [Fact]
    public async Task AdminUpdateAuthor_WhenUnauthorized_ReturnsUnauthorized()
    {
        //Arrange
        var request = AuthorData.UpdateAuthorByAdminRequest;

        //Act
        var response = await Client.PutAsync($"api/authors/{AdminIds.AuthorId}", request);

        //Assert
        response.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
    }

    [Fact]
    public async Task AdminUpdateAuthor_WhenInvalid_ReturnsBadRequest()
    {
        //Arrange
        var request = AuthorData.InvalidAuthorByAdminRequest;

        //Act
        await Client.AdminLoginAsync();

        var response = await Client.PutAsync($"api/authors/{AdminIds.AuthorId}", request);

        //Assert
        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
    }

    [Fact]
    public async Task AdminUpdateAuthor_WhenAuthorNotExists_ReturnsBadRequest()
    {
        //Arrange
        var request = AuthorData.UpdateAuthorByAdminRequest;

        //Act
        await Client.AdminLoginAsync();

        var response = await Client.PutAsync($"api/authors/{Guid.NewGuid()}", request);

        //Assert
        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
    }

    [Fact]
    public async Task AdminUpdateAuthor_WhenAuthorBelongsToUser_ReturnsBadRequest()
    {
        //Arrange
        var request = AuthorData.UpdateAuthorByAdminRequest;

        //Act
        await Client.AdminLoginAsync();

        var response = await Client.PutAsync($"api/authors/{UserIds.AuthorId}", request);

        //Assert
        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
    }

    [Fact]
    public async Task UserUpdateAuthor_WhenAllOk_ReturnsOk()
    {
        //Arrange
        var request = AuthorData.UpdateAuthorByUserRequest;

        //Act
        await Client.UserWithDataLoginAsync();

        var preGetResponse = await Client.GetAsync("api/authors/own");

        var preAuthorDto = await preGetResponse.ReadJson<AuthorDto>();

        var updateResponse = await Client.PutAsync("api/authors/own", request);

        var postGetResponse = await Client.GetAsync("api/authors/own");

        var postAuthorDto = await postGetResponse.ReadJson<AuthorDto>();

        //Assert
        preGetResponse.StatusCode.Should().Be(HttpStatusCode.OK);

        preAuthorDto!.Id.Should().Be(UserIds.AuthorId);
        preAuthorDto.Name.Should().Be(AuthorData.Name);
        preAuthorDto.Biography.Should().Be(AuthorData.Biography);
        preAuthorDto.BirthDate.Should().Be(AuthorData.BirthDate);
        preAuthorDto.DeathDate.Should().BeNull();

        updateResponse.StatusCode.Should().Be(HttpStatusCode.OK);

        postGetResponse.StatusCode.Should().Be(HttpStatusCode.OK);

        postAuthorDto!.Id.Should().Be(UserIds.AuthorId);
        postAuthorDto.Name.Should().Be(AuthorData.NewName);
        postAuthorDto.Biography.Should().Be(AuthorData.NewBiography);
        postAuthorDto.BirthDate.Should().Be(AuthorData.NewBirthDate);
        postAuthorDto.DeathDate.Should().BeNull();
    }

    [Fact]
    public async Task UserUpdateAuthor_WhenAdmin_ReturnsForbidden()
    {
        //Arrange
        var request = AuthorData.UpdateAuthorByUserRequest;

        //Act
        await Client.AdminLoginAsync();

        var updateResponse = await Client.PutAsync("api/authors/own", request);

        //Assert
        updateResponse.StatusCode.Should().Be(HttpStatusCode.Forbidden);
    }

    [Fact]
    public async Task UserUpdateAuthor_WhenUnauthorized_ReturnsUnauthorized()
    {
        //Arrange
        var request = AuthorData.UpdateAuthorByUserRequest;

        //Act
        var updateResponse = await Client.PutAsync("api/authors/own", request);

        //Assert
        updateResponse.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
    }

    [Fact]
    public async Task UserUpdateAuthor_WhenInvalid_ReturnsBadRequest()
    {
        //Arrange
        var request = AuthorData.InvalidAuthorByUserRequest;

        //Act
        await Client.UserWithDataLoginAsync();

        var updateResponse = await Client.PutAsync("api/authors/own", request);

        //Assert
        updateResponse.StatusCode.Should().Be(HttpStatusCode.BadRequest);
    }

    [Fact]
    public async Task UserUpdateAuthor_WhenAuthorNotExists_ReturnsBadRequest()
    {
        //Arrange
        var request = AuthorData.InvalidAuthorByUserRequest;

        //Act
        await Client.EmptyUserLoginAsync();

        var updateResponse = await Client.PutAsync("api/authors/own", request);

        //Assert
        updateResponse.StatusCode.Should().Be(HttpStatusCode.BadRequest);
    }
}