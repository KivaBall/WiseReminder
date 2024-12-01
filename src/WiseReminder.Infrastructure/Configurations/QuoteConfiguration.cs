namespace WiseReminder.Infrastructure.Configurations;

public sealed class QuoteConfiguration : IEntityTypeConfiguration<Quote>
{
    public void Configure(EntityTypeBuilder<Quote> builder)
    {
        builder.HasQueryFilter(quote => quote.DeletedAt == null);

        builder.Property(quote => quote.Text)
            .HasMaxLength(1024)
            .HasConversion(text => text.Value, value => new QuoteText(value));

        builder.Property(quote => quote.QuoteDate)
            .HasConversion(quoteDate => quoteDate.Value, value => new QuoteDate(value));
    }
}