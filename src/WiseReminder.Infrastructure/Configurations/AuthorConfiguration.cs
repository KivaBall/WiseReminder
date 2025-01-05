namespace WiseReminder.Infrastructure.Configurations;

public sealed class AuthorConfiguration : IEntityTypeConfiguration<Author>
{
    public void Configure(EntityTypeBuilder<Author> builder)
    {
        builder.ToTable("authors");

        builder.Property(a => a.Id)
            .HasColumnName("id");
        builder.HasKey(a => a.Id);

        builder.Property(a => a.AddedAt)
            .HasColumnName("added_at");

        builder.Property(a => a.IsDeleted)
            .HasColumnName("is_deleted");
        builder.HasQueryFilter(a => a.IsDeleted == false);

        builder.Property(c => c.DeletedAt)
            .HasColumnName("deleted_at");

        builder.Property(a => a.Name)
            .HasMaxLength(64)
            .HasConversion(name => name.Value, value => new AuthorName(value))
            .HasColumnName("name");

        builder.Property(a => a.Biography)
            .HasMaxLength(2048)
            .HasConversion(biography => biography.Value, value => new Biography(value))
            .HasColumnName("biography");

        builder.Property(a => a.BirthDate)
            .HasConversion(date => date.Value, value => Date.Create(value).ValueOrDefault)
            .HasColumnName("birth_date");

        builder.Property(a => a.DeathDate)
            .HasConversion(date => date == null ? (DateOnly?)null : date.Value,
                value => value == null ? null : Date.Create((DateOnly)value).ValueOrDefault)
            .HasColumnName("death_date");

        builder.Property(a => a.UserId)
            .HasColumnName("user_id");
    }
}