using WiseReminder.Domain.Abstractions;
using WiseReminder.Domain.Quotes;

namespace WiseReminder.Domain.Categories;

public sealed class Category : Entity
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

    public CategoryName Name { get; internal set; }
    public CategoryDescription Description { get; internal set; }
    public ICollection<Quote> Quotes { get; } = [];
}