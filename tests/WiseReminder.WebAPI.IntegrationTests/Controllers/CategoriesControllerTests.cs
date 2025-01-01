namespace WiseReminder.IntegrationTests.Controllers;

public sealed class CategoriesControllerTests : BaseControllerTests
{
    [Fact]
    public async Task CreateCategory_WhenAllOk_ReturnsOk()
    {
        //Arrange
        var request = CategoryData.CreateCategoryRequest;

        //Act
        await Client.AdminLoginAsync();

        var createResponse = await Client.PostAsJsonAsync("api/categories", request);

        var id = await createResponse.Content.ReadFromJsonAsync<Guid>();

        var getResponse = await Client.GetFromJsonAsync<CategoryDto>($"api/categories/{id}");

        //Assert
        createResponse.StatusCode.Should().Be(HttpStatusCode.OK);

        getResponse!.Id.Should().Be(id);
        getResponse.Name.Should().Be(CategoryData.DefaultName);
        getResponse.Description.Should().Be(CategoryData.DefaultDescription);
    }

    [Fact]
    public async Task CreateCategory_WhenUser_ReturnsForbidden()
    {
        //Arrange
        var request = CategoryData.CreateCategoryRequest;

        //Act
        await Client.UserLoginAsync();

        var response = await Client.PostAsJsonAsync("api/categories", request);

        //Assert
        response.StatusCode.Should().Be(HttpStatusCode.Forbidden);
    }

    [Fact]
    public async Task CreateCategory_WhenUnauthorized_ReturnsUnauthorized()
    {
        //Arrange
        var request = CategoryData.CreateCategoryRequest;

        //Act
        var response = await Client.PostAsJsonAsync("api/categories", request);

        //Assert
        response.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
    }

    [Fact]
    public async Task CreateCategory_WhenInvalid_ReturnsBadRequest()
    {
        //Arrange
        var request = CategoryData.InvalidCategoryRequest;

        //Act
        await Client.AdminLoginAsync();

        var response = await Client.PostAsJsonAsync("api/categories", request);

        //Assert
        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
    }

    [Fact]
    public async Task UpdateCategory_WhenAllOk_ReturnsOk()
    {
        //Arrange
        var request = CategoryData.UpdateCategoryRequest;

        //Act
        await Client.AdminLoginAsync();

        var updateResponse =
            await Client.PutAsJsonAsync($"api/categories/{Ids.CategoryId}", request);

        var getResponse =
            await Client.GetFromJsonAsync<CategoryDto>($"api/categories/{Ids.CategoryId}");

        //Assert
        updateResponse.StatusCode.Should().Be(HttpStatusCode.OK);

        getResponse!.Id.Should().Be(Ids.CategoryId);
        getResponse.Name.Should().Be(CategoryData.UpdatedName);
        getResponse.Description.Should().Be(CategoryData.UpdatedDescription);
    }

    [Fact]
    public async Task UpdateCategory_WhenUser_ReturnsForbidden()
    {
        //Arrange
        var request = CategoryData.UpdateCategoryRequest;

        //Act
        await Client.UserLoginAsync();

        var response = await Client.PutAsJsonAsync($"api/categories/{Ids.CategoryId}", request);

        //Assert
        response.StatusCode.Should().Be(HttpStatusCode.Forbidden);
    }

    [Fact]
    public async Task UpdateCategory_WhenUnauthorized_ReturnsUnauthorized()
    {
        //Arrange
        var request = CategoryData.UpdateCategoryRequest;

        //Act
        var response = await Client.PutAsJsonAsync($"api/categories/{Ids.CategoryId}", request);

        //Assert
        response.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
    }

    [Fact]
    public async Task UpdateCategory_WhenCategoryNotExists_ReturnsBadRequest()
    {
        //Arrange
        var request = CategoryData.UpdateCategoryRequest;

        //Act
        await Client.AdminLoginAsync();

        var response = await Client.PutAsJsonAsync($"api/categories/{Guid.NewGuid()}", request);

        //Assert
        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
    }

    [Fact]
    public async Task UpdateCategory_WhenInvalid_ReturnsBadRequest()
    {
        //Arrange
        var request = CategoryData.InvalidCategoryRequest;

        //Act
        await Client.AdminLoginAsync();

        var response = await Client.PutAsJsonAsync($"api/categories/{Ids.CategoryId}", request);

        //Assert
        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
    }

    [Fact]
    public async Task DeleteCategory_WhenAllOk_ReturnsOk()
    {
        //Act
        await Client.AdminLoginAsync();

        var deleteResponse = await Client
            .DeleteAsync($"api/categories/{Ids.CategoryId}");

        var getResponse = await Client
            .GetAsync($"api/categories/{Ids.CategoryId}");

        //Assert
        deleteResponse.StatusCode.Should().Be(HttpStatusCode.OK);

        getResponse.StatusCode.Should().Be(HttpStatusCode.NotFound);
    }

    [Fact]
    public async Task DeleteCategory_WhenUser_ReturnsForbidden()
    {
        //Act
        await Client.UserLoginAsync();

        var response = await Client.DeleteAsync($"api/categories/{Ids.CategoryId}");

        //Assert
        response.StatusCode.Should().Be(HttpStatusCode.Forbidden);
    }

    [Fact]
    public async Task DeleteCategory_WhenUnauthorized_ReturnsUnauthorized()
    {
        //Act
        var response = await Client.DeleteAsync($"api/categories/{Ids.CategoryId}");

        //Assert
        response.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
    }

    [Fact]
    public async Task DeleteCategory_WhenCategoryNotExists_ReturnsBadRequest()
    {
        //Act
        await Client.AdminLoginAsync();

        var response = await Client.DeleteAsync($"api/categories/{Guid.NewGuid()}");

        //Assert
        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
    }

    [Fact]
    public async Task GetCategories_WhenAllOk_ReturnsOk()
    {
        //Act
        var response = await Client.GetAsync("api/categories");

        //Assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);
    }

    [Fact]
    public async Task GetCategoryById_WhenAllOk_ReturnsOk()
    {
        //Act
        var response = await Client.GetAsync($"api/categories/{Ids.CategoryId}");

        var category = await response.Content.ReadFromJsonAsync<CategoryDto>();

        //Assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);

        category!.Name.Should().Be(CategoryData.DefaultName);
        category.Description.Should().Be(CategoryData.DefaultDescription);
    }

    [Fact]
    public async Task GetCategoryById_WhenCategoryNotExists_ReturnsNotFound()
    {
        //Act
        var response = await Client.GetAsync($"api/categories/{Guid.NewGuid()}");

        //Assert
        response.StatusCode.Should().Be(HttpStatusCode.NotFound);
    }
}