namespace WiseReminder.IntegrationTests.Controllers.Categories;

public sealed class CategoriesControllerGetMethodsTests : BaseControllerTests
{
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