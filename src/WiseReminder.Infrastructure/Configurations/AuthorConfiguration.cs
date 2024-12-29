namespace WiseReminder.Infrastructure.Configurations;

public sealed class AuthorConfiguration : IEntityTypeConfiguration<Author>
{
    public void Configure(EntityTypeBuilder<Author> builder)
    {
        builder.HasKey(a => a.Id);
        builder.Property(a => a.AddedAt);

        builder.HasQueryFilter(a => a.DeletedAt == null);
        builder.HasQueryFilter(a => a.IsDeleted == false);

        builder.Property(a => a.Name)
            .HasMaxLength(64)
            .HasConversion(name => name.Value, value => new AuthorName(value));

        builder.Property(a => a.Biography)
            .HasMaxLength(2048)
            .HasConversion(biography => biography.Value, value => new Biography(value));

        builder.Property(a => a.BirthDate)
            .HasConversion(date => date.Value, value => Date.Create(value).ValueOrDefault);

        builder.Property(a => a.DeathDate)
            .HasConversion(date => date == null ? (DateOnly?)null : date.Value,
                value => value == null ? null : Date.Create((DateOnly)value).ValueOrDefault);

        builder
            .HasOne(a => a.User)
            .WithOne(u => u.Author)
            .HasForeignKey<Author>(a => a.UserId)
            .IsRequired(false);
    }
}