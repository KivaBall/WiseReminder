namespace WiseReminder.Infrastructure.Configurations;

public sealed class QuoteConfiguration : BaseEntityConfiguration<Quote>
{
    public override void Configure(EntityTypeBuilder<Quote> builder)
    {
        base.Configure(builder);

        ApplyPrimaryKey(builder);

        builder.ToTable("quotes");

        builder.Property(q => q.Text)
            .HasMaxLength(1024)
            .HasConversion(text => text.Value, value => new Text(value))
            .HasColumnName("text");

        builder.Property(q => q.QuoteDate)
            .HasConversion(date => date.Value, value => Date.Create(value).ValueOrDefault)
            .HasColumnName("quote_date");

        builder.Property(q => q.AuthorId)
            .HasColumnName("author_id");

        builder.Property(q => q.CategoryId)
            .HasColumnName("category_id");

        builder.HasOne<Category>().WithMany()
            .HasForeignKey(q => q.CategoryId);

        builder.HasOne<Author>().WithMany()
            .HasForeignKey(q => q.AuthorId);

        builder.HasMany<Reaction>().WithOne()
            .HasForeignKey(r => r.QuoteId);

        builder.HasIndex(q => q.CategoryId);

        builder.HasIndex(q => q.AuthorId);
    }
}