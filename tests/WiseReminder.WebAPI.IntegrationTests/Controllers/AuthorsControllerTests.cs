namespace WiseReminder.IntegrationTests.Controllers;

public sealed class AuthorsControllerTests : BaseControllerTests
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
        authorDto.DeathDate.Should().Be(AuthorData.DefaultDeathDate);
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

    [Fact]
    public async Task AdminUpdateAuthor_WhenAllOk_ReturnsOk()
    {
        //Arrange
        var request = AuthorData.UpdateAdminAuthorRequest;

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
        preAuthorDto.Name.Should().Be(AuthorData.DefaultName);
        preAuthorDto.Biography.Should().Be(AuthorData.DefaultBiography);
        preAuthorDto.BirthDate.Should().Be(AuthorData.DefaultBirthDate);
        preAuthorDto.DeathDate.Should().Be(AuthorData.DefaultDeathDate);
        preAuthorDto.UserId.Should().BeNull();

        updateResponse.StatusCode.Should().Be(HttpStatusCode.OK);

        postGetResponse.StatusCode.Should().Be(HttpStatusCode.OK);

        postAuthorDto!.Id.Should().Be(AdminIds.AuthorId);
        postAuthorDto.Name.Should().Be(AuthorData.UpdatedName);
        postAuthorDto.Biography.Should().Be(AuthorData.UpdatedBiography);
        postAuthorDto.BirthDate.Should().Be(AuthorData.UpdatedBirthDate);
        postAuthorDto.DeathDate.Should().Be(AuthorData.UpdatedDeathDate);
        postAuthorDto.UserId.Should().BeNull();
    }

    [Fact]
    public async Task AdminUpdateAuthor_WhenUser_ReturnsForbidden()
    {
        //Arrange
        var request = AuthorData.UpdateAdminAuthorRequest;

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
        var request = AuthorData.UpdateAdminAuthorRequest;

        //Act
        var response = await Client.PutAsync($"api/authors/{AdminIds.AuthorId}", request);

        //Assert
        response.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
    }

    [Fact]
    public async Task AdminUpdateAuthor_WhenInvalid_ReturnsBadRequest()
    {
        //Arrange
        var request = AuthorData.InvalidAdminAuthorRequest;

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
        var request = AuthorData.UpdateAdminAuthorRequest;

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
        var request = AuthorData.UpdateAdminAuthorRequest;

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
        var request = AuthorData.UpdateUserAuthorRequest;

        //Act
        await Client.UserWithDataLoginAsync();

        var preGetResponse = await Client.GetAsync($"api/authors/own");

        var preAuthorDto = await preGetResponse.ReadJson<AuthorDto>();

        var updateResponse = await Client.PutAsync($"api/authors/own", request);

        var postGetResponse = await Client.GetAsync($"api/authors/own");

        var postAuthorDto = await postGetResponse.ReadJson<AuthorDto>();

        //Assert
        preGetResponse.StatusCode.Should().Be(HttpStatusCode.OK);

        preAuthorDto!.Id.Should().Be(UserIds.AuthorId);
        preAuthorDto.Name.Should().Be(AuthorData.DefaultName);
        preAuthorDto.Biography.Should().Be(AuthorData.DefaultBiography);
        preAuthorDto.BirthDate.Should().Be(AuthorData.DefaultBirthDate);
        preAuthorDto.DeathDate.Should().BeNull();
        preAuthorDto.UserId.Should().Be(UserIds.UserId);

        updateResponse.StatusCode.Should().Be(HttpStatusCode.OK);

        postGetResponse.StatusCode.Should().Be(HttpStatusCode.OK);

        postAuthorDto!.Id.Should().Be(UserIds.AuthorId);
        postAuthorDto.Name.Should().Be(AuthorData.UpdatedName);
        postAuthorDto.Biography.Should().Be(AuthorData.UpdatedBiography);
        postAuthorDto.BirthDate.Should().Be(AuthorData.UpdatedBirthDate);
        postAuthorDto.DeathDate.Should().BeNull();
        postAuthorDto.UserId.Should().Be(UserIds.UserId);
    }

    [Fact]
    public async Task UserUpdateAuthor_WhenAdmin_ReturnsForbidden()
    {
        //Arrange
        var request = AuthorData.UpdateUserAuthorRequest;

        //Act
        await Client.AdminLoginAsync();

        var updateResponse = await Client.PutAsync($"api/authors/own", request);

        //Assert
        updateResponse.StatusCode.Should().Be(HttpStatusCode.Forbidden);
    }

    [Fact]
    public async Task UserUpdateAuthor_WhenUnauthorized_ReturnsUnauthorized()
    {
        //Arrange
        var request = AuthorData.UpdateUserAuthorRequest;

        //Act
        var updateResponse = await Client.PutAsync($"api/authors/own", request);

        //Assert
        updateResponse.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
    }

    [Fact]
    public async Task UserUpdateAuthor_WhenInvalid_ReturnsBadRequest()
    {
        //Arrange
        var request = AuthorData.InvalidUserAuthorRequest;

        //Act
        await Client.UserWithDataLoginAsync();

        var updateResponse = await Client.PutAsync($"api/authors/own", request);

        //Assert
        updateResponse.StatusCode.Should().Be(HttpStatusCode.BadRequest);
    }

    [Fact]
    public async Task UserUpdateAuthor_WhenAuthorNotExists_ReturnsBadRequest()
    {
        //Arrange
        var request = AuthorData.InvalidUserAuthorRequest;

        //Act
        await Client.EmptyUserLoginAsync();

        var updateResponse = await Client.PutAsync($"api/authors/own", request);

        //Assert
        updateResponse.StatusCode.Should().Be(HttpStatusCode.BadRequest);
    }

    [Fact]
    public async Task AdminDeleteAuthor_WhenAllOk_ReturnsOk()
    {
        //Act
        await Client.AdminLoginAsync();

        var preGetResponse = await Client.DeleteAsync($"api/authors/{AdminIds.AuthorId}");

        var deleteResponse = await Client.DeleteAsync($"api/authors/{AdminIds.AuthorId}");

        var postGetResponse = await Client.GetAsync($"api/authors/{AdminIds.AuthorId}");

        //Assert
        preGetResponse.StatusCode.Should().Be(HttpStatusCode.OK);
        
        deleteResponse.StatusCode.Should().Be(HttpStatusCode.OK);

        postGetResponse.StatusCode.Should().Be(HttpStatusCode.NotFound);

        //TODO: What will be with quotes?
    }

    [Fact]
    public async Task AdminDeleteAuthor_WhenUser_ReturnsForbidden()
    {
        //Act
        await Client.EmptyUserLoginAsync();

        var response = await Client.DeleteAsync($"api/authors/{AdminIds.AuthorId}");

        //Assert
        response.StatusCode.Should().Be(HttpStatusCode.Forbidden);
    }

    [Fact]
    public async Task AdminDeleteAuthor_WhenUnauthorized_ReturnsUnauthorized()
    {
        //Act
        var response = await Client.DeleteAsync($"api/authors/{AdminIds.AuthorId}");

        //Assert
        response.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
    }

    [Fact]
    public async Task AdminDeleteAuthor_WhenAuthorNotExists_ReturnsBadRequest()
    {
        //Act
        await Client.AdminLoginAsync();

        var response = await Client.DeleteAsync($"api/authors/{Guid.NewGuid()}");

        //Assert
        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
    }

    [Fact]
    public async Task AdminDeleteAuthor_WhenAuthorBelongsToUser_ReturnsBadRequest()
    {
        //Act
        await Client.AdminLoginAsync();

        var response = await Client.DeleteAsync($"api/authors/{UserIds.AuthorId}");

        //Assert
        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
    }

    [Fact]
    public async Task UserDeleteAuthor_WhenAllOk_ReturnsOk()
    {
        //Act
        await Client.UserWithDataLoginAsync();

        var preGetResponse = await Client.GetAsync($"api/authors/own");

        var preAuthorDto = await preGetResponse.ReadJson<AuthorDto>();

        var deleteResponse = await Client.DeleteAsync($"api/authors/own");

        var postGetResponse = await Client.GetAsync($"api/authors/own");

        //Assert
        preGetResponse.StatusCode.Should().Be(HttpStatusCode.OK);

        deleteResponse.StatusCode.Should().Be(HttpStatusCode.OK);

        postGetResponse.StatusCode.Should().Be(HttpStatusCode.NotFound);
    }

    [Fact]
    public async Task UserDeleteAuthor_WhenAdmin_ReturnsForbidden()
    {
        //Act
        await Client.AdminLoginAsync();

        var response = await Client.DeleteAsync($"api/authors/own");

        //Assert
        response.StatusCode.Should().Be(HttpStatusCode.Forbidden);
    }

    [Fact]
    public async Task UserDeleteAuthor_WhenUnauthorized_ReturnsUnauthorized()
    {
        //Act
        var response = await Client.DeleteAsync($"api/authors/own");

        //Assert
        response.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
    }

    [Fact]
    public async Task UserDeleteAuthor_WhenAuthorNotExists_ReturnsBadRequest()
    {
        //Act
        await Client.EmptyUserLoginAsync();

        var deleteResponse = await Client.DeleteAsync($"api/authors/own");

        //Assert
        deleteResponse.StatusCode.Should().Be(HttpStatusCode.BadRequest);
    }

    [Fact]
    public async Task GetAllAuthors_WhenAllOk_ReturnsOk()
    {
        //Act
        var response = await Client.GetAsync("api/authors");

        //Assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);
    }

    [Fact]
    public async Task GetAuthorById_WhenAllOk_ReturnsOk()
    {
        //Act
        var response = await Client.GetAsync($"api/authors/{AdminIds.AuthorId}");

        var authorDto = await response.ReadJson<AuthorDto>();

        //Assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);

        authorDto!.Name.Should().Be(AuthorData.DefaultName);
        authorDto.Biography.Should().Be(AuthorData.DefaultBiography);
        authorDto.BirthDate.Should().Be(AuthorData.DefaultBirthDate);
        authorDto.DeathDate.Should().Be(AuthorData.DefaultDeathDate);
    }

    [Fact]
    public async Task GetAuthorById_WhenAuthorNotExists_ReturnsNotFound()
    {
        //Act
        var response = await Client.GetAsync($"api/authors/{Guid.NewGuid()}");

        //Assert
        response.StatusCode.Should().Be(HttpStatusCode.NotFound);
    }

    [Fact]
    public async Task GetMyOwnAuthor_WhenAllOk_ReturnsOk()
    {
        //Act
        await Client.UserWithDataLoginAsync();

        var response = await Client.GetAsync($"api/authors/own");

        var authorDto = await response.ReadJson<AuthorDto>();

        //Assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);

        authorDto!.Id.Should().Be(UserIds.AuthorId);
        authorDto.Name.Should().Be(AuthorData.DefaultName);
        authorDto.Biography.Should().Be(AuthorData.DefaultBiography);
        authorDto.BirthDate.Should().Be(AuthorData.DefaultBirthDate);
        authorDto.DeathDate.Should().BeNull();
        authorDto.UserId.Should().Be(UserIds.UserId);
    }

    [Fact]
    public async Task GetMyOwnAuthor_WhenAuthorNotExists_ReturnsNotFound()
    {
        //Act
        await Client.EmptyUserLoginAsync();

        var response = await Client.GetAsync($"api/authors/own");

        //Assert
        response.StatusCode.Should().Be(HttpStatusCode.NotFound);
    }

    [Fact]
    public async Task GetAuthorByUserId_WhenAllOk_ReturnsOk()
    {
        //Act
        await Client.AdminLoginAsync();

        var response = await Client.GetAsync($"api/authors/user/{UserIds.UserId}");

        var authorDto = await response.ReadJson<AuthorDto>();

        //Assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);

        authorDto!.Id.Should().Be(UserIds.AuthorId);
        authorDto.Name.Should().Be(AuthorData.DefaultName);
        authorDto.Biography.Should().Be(AuthorData.DefaultBiography);
        authorDto.BirthDate.Should().Be(AuthorData.DefaultBirthDate);
        authorDto.DeathDate.Should().BeNull();
        authorDto.UserId.Should().Be(UserIds.UserId);
    }

    [Fact]
    public async Task GetAuthorByUserId_WhenUser_ReturnsForbidden()
    {
        //Act
        await Client.EmptyUserLoginAsync();

        var response = await Client.GetAsync($"api/authors/user/{UserIds.UserId}");

        //Assert
        response.StatusCode.Should().Be(HttpStatusCode.Forbidden);
    }

    [Fact]
    public async Task GetAuthorByUserId_WhenUnauthorized_ReturnsUnauthorized()
    {
        //Act
        var response = await Client.GetAsync($"api/authors/user/{UserIds.UserId}");

        //Assert
        response.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
    }

    [Fact]
    public async Task GetAuthorByUserId_WhenAuthorNotExists_ReturnsBadRequest()
    {
        //Act
        await Client.AdminLoginAsync();

        var response = await Client.GetAsync($"api/authors/user/{AdminIds.UserId}");

        //Assert
        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
    }

    [Fact]
    public async Task GetAuthorByUserId_WhenUserNotExists_ReturnsBadRequest()
    {
        //Act
        await Client.AdminLoginAsync();

        var response = await Client.GetAsync($"api/authors/user/{Guid.NewGuid()}");

        //Assert
        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
    }
}