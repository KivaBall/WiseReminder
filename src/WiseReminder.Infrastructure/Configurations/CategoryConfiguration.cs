namespace WiseReminder.Infrastructure.Configurations;

public sealed class CategoryConfiguration : IEntityTypeConfiguration<Category>
{
    public void Configure(EntityTypeBuilder<Category> builder)
    {
        builder.HasQueryFilter(category => category.DeletedAt == null);

        builder.Property(category => category.Name)
            .HasMaxLength(64)
            .HasConversion(name => name.Value, value => new CategoryName(value));

        builder.Property(category => category.Description)
            .HasMaxLength(1024)
            .HasConversion(description => description.Value, value => new CategoryDescription(value));
    }
}