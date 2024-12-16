namespace WiseReminder.Domain.Categories;

public sealed class Category : Entity<Category>
{
    public Category(CategoryName name, CategoryDescription description)
    {
        Name = name;
        Description = description;
    }

    // ReSharper disable once UnusedMember.Local
    private Category()
    {
    }

    public CategoryName Name { get; private set; }
    public CategoryDescription Description { get; private set; }

    public ICollection<Quote> Quotes { get; } = [];

    public Category Update(CategoryName name, CategoryDescription description)
    {
        Name = name;
        Description = description;

        return this;
    }
}