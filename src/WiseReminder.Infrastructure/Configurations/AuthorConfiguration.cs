using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WiseReminder.Domain.Authors;

namespace WiseReminder.Infrastructure.Configurations;

public sealed class AuthorConfiguration : IEntityTypeConfiguration<Author>
{
    public void Configure(EntityTypeBuilder<Author> builder)
    {
        builder.HasQueryFilter(author => author.DeletedAt == null);

        builder.Property(author => author.Name)
            .HasMaxLength(64)
            .HasConversion(name => name.Value, value => new AuthorName(value));

        builder.Property(author => author.Biography)
            .HasMaxLength(2048)
            .HasConversion(biography => biography.Value, value => new AuthorBiography(value));

        builder.Property(author => author.DateOfBirth)
            .HasConversion(dateOfBirth => dateOfBirth.Value, value => new AuthorDateOfBirth(value));

        builder.Property(author => author.DateOfDeath)
            .HasConversion(dateOfDeath => dateOfDeath.Value, value => new AuthorDateOfDeath(value));
    }
}