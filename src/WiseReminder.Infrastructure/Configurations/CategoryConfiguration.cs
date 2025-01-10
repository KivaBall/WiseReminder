namespace WiseReminder.Infrastructure.Configurations;

public sealed class CategoryConfiguration : BaseEntityConfiguration<Category>
{
    public override void Configure(EntityTypeBuilder<Category> builder)
    {
        base.Configure(builder);

        ApplyPrimaryKey(builder);

        builder.ToTable("categories");

        builder.Property(c => c.Name)
            .HasMaxLength(64)
            .HasConversion(name => name.Value, value => new CategoryName(value))
            .HasColumnName("name");

        builder.Property(c => c.Description)
            .HasMaxLength(1024)
            .HasConversion(description => description.Value, value => new Description(value))
            .HasColumnName("description");

        builder.HasMany<Quote>().WithOne()
            .HasForeignKey(c => c.CategoryId);
    }
}