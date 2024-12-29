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
            .HasConversion(text => text.Value, value => new Text(value));

        builder.Property(q => q.QuoteDate)
            .HasConversion(date => date.Value, value => Date.Create(value).ValueOrDefault);
    }
}