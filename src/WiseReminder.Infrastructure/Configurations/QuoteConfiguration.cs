namespace WiseReminder.Infrastructure.Configurations;

public sealed class QuoteConfiguration : IEntityTypeConfiguration<Quote>
{
    public void Configure(EntityTypeBuilder<Quote> builder)
    {
        builder.HasKey(q => q.Id);
        builder.Property(a => a.AddedAt);

        builder.HasQueryFilter(q => q.DeletedAt == null);
        builder.HasQueryFilter(q => q.IsDeleted == false);

        builder.Property(q => q.Text)
            .HasMaxLength(1024)
            .HasConversion(text => text.Value, value => new QuoteText(value));

        builder.OwnsOne(q => q.QuoteDate, dateBuilder =>
        {
            dateBuilder.Property(d => d.Year)
                .HasConversion(year => year, year => year)
                .HasColumnName("QuoteYear");

            dateBuilder.Property(d => d.Month)
                .HasConversion(month => month, month => month)
                .HasColumnName("QuoteMonth");

            dateBuilder.Property(d => d.Day)
                .HasConversion(day => day, day => day)
                .HasColumnName("QuoteDay");
        });
    }
}