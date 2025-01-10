namespace WiseReminder.Infrastructure.Configurations;

public sealed class AuthorConfiguration : BaseEntityConfiguration<Author>
{
    public override void Configure(EntityTypeBuilder<Author> builder)
    {
        base.Configure(builder);

        ApplyPrimaryKey(builder);

        builder.ToTable("authors");

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

        builder.HasMany<Quote>().WithOne()
            .HasForeignKey(q => q.AuthorId);

        builder.HasOne<User>().WithOne()
            .HasForeignKey<Author>(a => a.UserId);

        builder.HasIndex(a => a.UserId);
    }
}