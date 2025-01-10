namespace WiseReminder.Infrastructure.Configurations;

public sealed class UserConfiguration : BaseEntityConfiguration<User>
{
    public override void Configure(EntityTypeBuilder<User> builder)
    {
        base.Configure(builder);

        ApplyPrimaryKey(builder);

        builder.ToTable("users");

        builder.Property(u => u.Username)
            .HasMaxLength(64)
            .HasConversion(username => username.Value, value => new Username(value))
            .HasColumnName("username");

        builder.Property(u => u.Login)
            .HasMaxLength(64)
            .HasConversion(login => login.Value, value => new Login(value))
            .HasColumnName("login");

        builder.Property(u => u.HashedPassword)
            .HasMaxLength(64)
            .HasConversion(hashedPassword => hashedPassword.Value,
                value => new HashedPassword(value))
            .HasColumnName("hashed_password");

        builder.Property(u => u.Subscription)
            .HasColumnName("subscription");

        builder.HasMany<Reaction>().WithOne()
            .HasForeignKey(r => r.UserId);

        builder.HasIndex(u => u.Login)
            .IsUnique();
    }
}