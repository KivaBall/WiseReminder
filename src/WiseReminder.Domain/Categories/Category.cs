namespace WiseReminder.Domain.Categories;

public sealed class Category : Entity<Category>
{
    public Category(CategoryName name, Description description)
    {
        Name = name;
        Description = description;
    }

    public CategoryName Name { get; private set; }
    public Description Description { get; private set; }

    public ICollection<Quote> Quotes { get; } = [];

    public Category Update(CategoryName name, Description description)
    {
        Name = name;
        Description = description;

        return this;
    }
}