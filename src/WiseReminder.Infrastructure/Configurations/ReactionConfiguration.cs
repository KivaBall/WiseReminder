namespace WiseReminder.Infrastructure.Configurations;

public sealed class ReactionConfiguration : BaseEntityConfiguration<Reaction>
{
    public override void Configure(EntityTypeBuilder<Reaction> builder)
    {
        base.Configure(builder);

        builder.ToTable("reactions");

        builder.Property(r => r.QuoteId)
            .HasColumnName("quote_id");

        builder.Property(r => r.UserId)
            .HasColumnName("user_id");

        builder.HasKey(r => new { r.QuoteId, r.UserId });

        builder.Property(r => r.IsLike)
            .HasConversion(isLike => isLike.Value, value => new IsLike(value))
            .HasColumnName("is_like");

        builder.HasOne<Quote>().WithMany()
            .HasForeignKey(r => r.QuoteId);

        builder.HasOne<User>().WithMany()
            .HasForeignKey(r => r.UserId);

        builder.HasIndex(r => new { r.QuoteId, r.UserId });

        builder.HasIndex(r => r.QuoteId);

        builder.HasIndex(r => r.UserId);
    }
}