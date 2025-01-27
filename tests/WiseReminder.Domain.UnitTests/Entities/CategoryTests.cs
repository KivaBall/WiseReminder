namespace WiseReminder.Domain.UnitTests.Entities;

public sealed class CategoryTests
{
    [Fact]
    public void CreateCategory()
    {
        //Arrange
        var categoryName = CategoryData.CategoryName;

        var description = CategoryData.Description;

        //Act
        var category = new Category(categoryName, description);

        //Assert
        category.Name.Should().Be(CategoryData.CategoryName);
        category.Description.Should().Be(CategoryData.Description);
    }

    [Fact]
    public void UpdateCategory()
    {
        //Arrange
        var category = CategoryData.Category;

        var categoryName = CategoryData.NewCategoryName;

        var description = CategoryData.NewDescription;

        //Act
        category.Update(categoryName, description);

        //Assert
        category.Name.Should().Be(CategoryData.NewCategoryName);
        category.Description.Should().Be(CategoryData.NewDescription);
    }
}