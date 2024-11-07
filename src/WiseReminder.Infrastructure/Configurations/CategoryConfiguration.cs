using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WiseReminder.Domain.Categories;

namespace WiseReminder.Infrastructure.Configurations;

public sealed class CategoryConfiguration : IEntityTypeConfiguration<Category>
{
    public void Configure(EntityTypeBuilder<Category> builder)
    {
        builder.Property(author => author.Name)
            .HasMaxLength(64)
            .HasConversion(name => name.Value, value => new CategoryName(value));

        builder.Property(author => author.Description)
            .HasMaxLength(1024)
            .HasConversion(description => description.Value, value => new CategoryDescription(value));
    }
}