using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WiseReminder.Domain.Quotes;

namespace WiseReminder.Infrastructure.Configurations;

public sealed class QuoteConfiguration : IEntityTypeConfiguration<Quote>
{
    public void Configure(EntityTypeBuilder<Quote> builder)
    {
        builder.Property(author => author.Text)
            .HasMaxLength(1024)
            .HasConversion(text => text.Value, value => new QuoteText(value));

        builder.Property(author => author.QuoteDate)
            .HasConversion(quoteDate => quoteDate.Value, value => new QuoteDate(value));
    }
}