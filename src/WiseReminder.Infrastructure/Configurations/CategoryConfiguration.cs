namespace WiseReminder.Infrastructure.Configurations;

public sealed class CategoryConfiguration : IEntityTypeConfiguration<Category>
{
    public void Configure(EntityTypeBuilder<Category> builder)
    {
        builder.ToTable("categories");

        builder.Property(c => c.Id)
            .HasColumnName("id");
        builder.HasKey(c => c.Id);

        builder.Property(c => c.AddedAt)
            .HasColumnName("added_at");

        builder.Property(c => c.IsDeleted)
            .HasColumnName("is_deleted");
        builder.HasQueryFilter(c => c.IsDeleted == false);

        builder.Property(c => c.DeletedAt)
            .HasColumnName("deleted_at");

        builder.Property(c => c.Name)
            .HasMaxLength(64)
            .HasConversion(name => name.Value, value => new CategoryName(value))
            .HasColumnName("name");

        builder.Property(c => c.Description)
            .HasMaxLength(1024)
            .HasConversion(description => description.Value, value => new Description(value))
            .HasColumnName("description");
    }
}