namespace WiseReminder.Infrastructure.Configurations;

public sealed class CategoryConfiguration : IEntityTypeConfiguration<Category>
{
    public void Configure(EntityTypeBuilder<Category> builder)
    {
        builder.HasKey(c => c.Id);
        builder.Property(a => a.AddedAt);

        builder.HasQueryFilter(c => c.DeletedAt == null);
        builder.HasQueryFilter(c => c.IsDeleted == false);

        builder.Property(c => c.Name)
            .HasMaxLength(64)
            .HasConversion(name => name.Value, value => new CategoryName(value));

        builder.Property(c => c.Description)
            .HasMaxLength(1024)
            .HasConversion(description => description.Value, value => new Description(value));
    }
}