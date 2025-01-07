namespace WiseReminder.Infrastructure.Configurations;

public sealed class QuoteConfiguration : IEntityTypeConfiguration<Quote>
{
    public void Configure(EntityTypeBuilder<Quote> builder)
    {
        builder.ToTable("quotes");

        builder.Property(q => q.Id)
            .HasColumnName("id");
        builder.HasKey(q => q.Id);

        builder.Property(q => q.AddedAt)
            .HasColumnName("added_at");

        builder.Property(q => q.IsDeleted)
            .HasColumnName("is_deleted");
        builder.HasQueryFilter(q => q.IsDeleted == false);

        builder.Property(q => q.DeletedAt)
            .HasColumnName("deleted_at");

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
    }
}