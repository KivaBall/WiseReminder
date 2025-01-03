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

        var createResponse = await Client.PostAsync("api/categories", request);

        var id = await createResponse.ReadJson<Guid>();

        var getResponse = await Client.GetAsync($"api/categories/{id}");

        var categoryDto = await getResponse.ReadJson<CategoryDto>();

        //Assert
        createResponse.StatusCode.Should().Be(HttpStatusCode.OK);

        getResponse.StatusCode.Should().Be(HttpStatusCode.OK);

        categoryDto!.Id.Should().Be(id);
        categoryDto.Name.Should().Be(CategoryData.DefaultName);
        categoryDto.Description.Should().Be(CategoryData.DefaultDescription);
    }

    [Fact]
    public async Task CreateCategory_WhenUser_ReturnsForbidden()
    {
        //Arrange
        var request = CategoryData.CreateCategoryRequest;

        //Act
        await Client.EmptyUserLoginAsync();

        var response = await Client.PostAsync("api/categories", request);

        //Assert
        response.StatusCode.Should().Be(HttpStatusCode.Forbidden);
    }

    [Fact]
    public async Task CreateCategory_WhenUnauthorized_ReturnsUnauthorized()
    {
        //Arrange
        var request = CategoryData.CreateCategoryRequest;

        //Act
        var response = await Client.PostAsync("api/categories", request);

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

        var response = await Client.PostAsync("api/categories", request);

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

        var preGetResponse = await Client.GetAsync($"api/categories/{AdminIds.CategoryId}");

        var preCategoryDto = await preGetResponse.ReadJson<CategoryDto>();

        var updateResponse =
            await Client.PutAsync($"api/categories/{AdminIds.CategoryId}", request);

        var postGetResponse = await Client.GetAsync($"api/categories/{AdminIds.CategoryId}");

        var postCategoryDto = await postGetResponse.ReadJson<CategoryDto>();

        //Assert
        preGetResponse.StatusCode.Should().Be(HttpStatusCode.OK);

        preCategoryDto!.Id.Should().Be(AdminIds.CategoryId);
        preCategoryDto.Name.Should().Be(CategoryData.DefaultName);
        preCategoryDto.Description.Should().Be(CategoryData.DefaultDescription);

        updateResponse.StatusCode.Should().Be(HttpStatusCode.OK);

        postGetResponse.StatusCode.Should().Be(HttpStatusCode.OK);

        postCategoryDto!.Id.Should().Be(AdminIds.CategoryId);
        postCategoryDto.Name.Should().Be(CategoryData.UpdatedName);
        postCategoryDto.Description.Should().Be(CategoryData.UpdatedDescription);
    }

    [Fact]
    public async Task UpdateCategory_WhenUser_ReturnsForbidden()
    {
        //Arrange
        var request = CategoryData.UpdateCategoryRequest;

        //Act
        await Client.EmptyUserLoginAsync();

        var response = await Client.PutAsync($"api/categories/{AdminIds.CategoryId}", request);

        //Assert
        response.StatusCode.Should().Be(HttpStatusCode.Forbidden);
    }

    [Fact]
    public async Task UpdateCategory_WhenUnauthorized_ReturnsUnauthorized()
    {
        //Arrange
        var request = CategoryData.UpdateCategoryRequest;

        //Act
        var response = await Client.PutAsync($"api/categories/{AdminIds.CategoryId}", request);

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

        var response = await Client.PutAsync($"api/categories/{Guid.NewGuid()}", request);

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

        var response = await Client.PutAsync($"api/categories/{AdminIds.CategoryId}", request);

        //Assert
        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
    }

    [Fact]
    public async Task DeleteCategory_WhenAllOk_ReturnsOk()
    {
        //Act
        await Client.AdminLoginAsync();

        var preGetResponse = await Client.GetAsync($"api/categories/{AdminIds.CategoryId}");
        
        var deleteResponse = await Client.DeleteAsync($"api/categories/{AdminIds.CategoryId}");

        var postGetResponse = await Client.GetAsync($"api/categories/{AdminIds.CategoryId}");

        //Assert
        preGetResponse.StatusCode.Should().Be(HttpStatusCode.OK);
        
        deleteResponse.StatusCode.Should().Be(HttpStatusCode.OK);

        postGetResponse.StatusCode.Should().Be(HttpStatusCode.NotFound);
        
        //TODO: Check what happened with quotes in category
    }

    [Fact]
    public async Task DeleteCategory_WhenUser_ReturnsForbidden()
    {
        //Act
        await Client.EmptyUserLoginAsync();

        var response = await Client.DeleteAsync($"api/categories/{AdminIds.CategoryId}");

        //Assert
        response.StatusCode.Should().Be(HttpStatusCode.Forbidden);
    }

    [Fact]
    public async Task DeleteCategory_WhenUnauthorized_ReturnsUnauthorized()
    {
        //Act
        var response = await Client.DeleteAsync($"api/categories/{AdminIds.CategoryId}");

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
    public async Task GetAllCategories_WhenAllOk_ReturnsOk()
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
        var response = await Client.GetAsync($"api/categories/{AdminIds.CategoryId}");

        var categoryDto = await response.ReadJson<CategoryDto>();

        //Assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);

        categoryDto!.Name.Should().Be(CategoryData.DefaultName);
        categoryDto.Description.Should().Be(CategoryData.DefaultDescription);
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