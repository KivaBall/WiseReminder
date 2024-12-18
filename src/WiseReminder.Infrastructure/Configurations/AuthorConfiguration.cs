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
            .HasConversion(biography => biography.Value, value => new AuthorBiography(value));

        builder.OwnsOne(a => a.BirthDate, dateBuilder =>
        {
            dateBuilder.Property(d => d.Year)
                .HasConversion(year => year, year => year)
                .HasColumnName("BirthYear");

            dateBuilder.Property(d => d.Month)
                .HasConversion(month => month, month => month)
                .HasColumnName("BirthMonth");

            dateBuilder.Property(d => d.Day)
                .HasConversion(day => day, day => day)
                .HasColumnName("BirthDay");
        });

        builder.OwnsOne(a => a.DeathDate, dateBuilder =>
        {
            dateBuilder.Property(d => d.Year)
                .HasConversion(year => year, year => year)
                .HasColumnName("DeathYear");

            dateBuilder.Property(d => d.Month)
                .HasConversion(month => month, month => month)
                .HasColumnName("DeathMonth");

            dateBuilder.Property(d => d.Day)
                .HasConversion(day => day, day => day)
                .HasColumnName("DeathDay");
        });
    }
}