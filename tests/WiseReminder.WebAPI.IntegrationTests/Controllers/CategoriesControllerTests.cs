namespace WiseReminder.IntegrationTests.Controllers;

public sealed class CategoriesControllerTests : GenericControllerTests
{
    [Fact]
    public async Task CreateCategory_WhenAllOk_ReturnsOk()
    {
        //Arrange
        await Client.LoginAsAdminAsync();
        var request = CategoryData.BaseCategoryRequest();

        //Act
        var createResponse = await Client.PostAsJsonAsync("api/categories", request);
        var categoryId = await createResponse.Content.ReadFromJsonAsync<Guid>();
        var getResponse =
            await Client.GetFromJsonAsync<CategoryDto>($"api/categories/{categoryId}");

        //Assert
        createResponse.StatusCode.Should().Be(HttpStatusCode.OK);
        getResponse!.Id.Should().Be(categoryId);
        getResponse.Name.Should().Be(CategoryData.DefaultName);
        getResponse.Description.Should().Be(CategoryData.DefaultDescription);
    }

    [Fact]
    public async Task CreateCategory_WhenUserNotAuthorized_ReturnsUnauthorized()
    {
        //Arrange
        var request = CategoryData.BaseCategoryRequest();

        //Act
        var response = await Client.PostAsJsonAsync("api/categories", request);

        //Assert
        response.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
    }

    [Fact]
    public async Task CreateCategory_WhenDataNotValid_ReturnsBadRequest()
    {
        //Arrange
        await Client.LoginAsAdminAsync();
        var request = CategoryData.NotValidBaseCategoryRequest();

        //Act
        var response = await Client.PostAsJsonAsync("api/categories", request);

        //Assert
        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
    }

    [Fact]
    public async Task UpdateCategory_WhenAllOk_ReturnsOk()
    {
        //Arrange
        await Client.LoginAsAdminAsync();
        var id = (await Client.SeedDataAsync()).CategoryId;
        var request = CategoryData.UpdateCategoryRequest(id);

        //Act
        var updateResponse = await Client.PutAsJsonAsync("api/categories", request);
        var getResponse =
            await Client.GetFromJsonAsync<CategoryDto>($"api/categories/{id}");

        //Assert
        updateResponse.StatusCode.Should().Be(HttpStatusCode.OK);
        getResponse!.Id.Should().Be(id);
        getResponse.Name.Should().Be(CategoryData.UpdatedName);
        getResponse.Description.Should().Be(CategoryData.UpdatedDescription);
    }

    [Fact]
    public async Task UpdateCategory_WhenUserNotAuthorized_ReturnsUnauthorized()
    {
        //Arrange
        var request = CategoryData.UpdateCategoryRequest(Guid.NewGuid());

        //Act
        var response = await Client.PutAsJsonAsync("api/categories", request);

        //Assert
        response.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
    }

    [Fact]
    public async Task UpdateCategory_WhenCategoryNotExisting_ReturnsBadRequest()
    {
        //Arrange
        await Client.LoginAsAdminAsync();
        var request = CategoryData.UpdateCategoryRequest(Guid.NewGuid());

        //Act
        var response = await Client.PutAsJsonAsync("api/categories", request);

        //Assert
        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
    }

    [Fact]
    public async Task UpdateCategory_WhenDataNotValid_ReturnsBadRequest()
    {
        //Arrange
        await Client.LoginAsAdminAsync();
        var id = (await Client.SeedDataAsync()).CategoryId;
        var request = CategoryData.NotValidUpdateCategoryRequest(id);

        //Act
        var response = await Client.PutAsJsonAsync("api/categories", request);

        //Assert
        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
    }

    [Fact]
    public async Task DeleteCategory_WhenAllOk_ReturnsOk()
    {
        //Arrange
        await Client.LoginAsAdminAsync();
        var id = (await Client.SeedDataAsync()).CategoryId;

        //Act
        var deleteResponse = await Client
            .DeleteAsync($"api/categories/{id}");
        var getResponse = await Client
            .GetAsync($"api/categories/{id}");

        //Assert
        deleteResponse.StatusCode.Should().Be(HttpStatusCode.OK);
        getResponse.StatusCode.Should().Be(HttpStatusCode.NotFound);
    }

    [Fact]
    public async Task DeleteCategory_WhenUserNotAuthorized_ReturnsUnauthorized()
    {
        //Arrange
        var id = Guid.NewGuid();

        //Act
        var response = await Client.DeleteAsync($"api/categories/{id}");

        //Assert
        response.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
    }

    [Fact]
    public async Task DeleteCategory_WhenCategoryNotExisting_ReturnsBadRequest()
    {
        //Arrange
        await Client.LoginAsAdminAsync();
        var id = Guid.NewGuid();

        //Act
        var response = await Client.DeleteAsync($"api/categories/{id}");

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
        //Arrange
        await Client.LoginAsAdminAsync();
        var id = (await Client.SeedDataAsync()).CategoryId;

        //Act
        var response = await Client.GetAsync($"api/categories/{id}");
        var category = await response.Content.ReadFromJsonAsync<CategoryDto>();

        //Assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);
        category!.Name.Should().Be(CategoryData.DefaultName);
        category.Description.Should().Be(CategoryData.DefaultDescription);
    }

    [Fact]
    public async Task GetCategoryById_WhenCategoryNotExisting_ReturnsNotFound()
    {
        //Arrange
        var id = Guid.NewGuid();

        //Act
        var response = await Client.GetAsync($"api/categories/{id}");

        //Assert
        response.StatusCode.Should().Be(HttpStatusCode.NotFound);
    }
}