namespace WiseReminder.Infrastructure.Configurations;

public sealed class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable("users");

        builder.Property(u => u.Id)
            .HasColumnName("id");
        builder.HasKey(u => u.Id);

        builder.Property(u => u.AddedAt)
            .HasColumnName("added_at");

        builder.Property(u => u.IsDeleted)
            .HasColumnName("is_deleted");
        builder.HasQueryFilter(u => u.IsDeleted == false);

        builder.Property(u => u.DeletedAt)
            .HasColumnName("deleted_at");

        builder.Property(u => u.Username)
            .HasMaxLength(64)
            .HasConversion(username => username.Value, value => new Username(value))
            .HasColumnName("username");

        builder.Property(u => u.Login)
            .HasMaxLength(64)
            .HasConversion(login => login.Value, value => new Login(value))
            .HasColumnName("login");
        builder.HasIndex(u => u.Login)
            .IsUnique();

        builder.Property(u => u.HashedPassword)
            .HasMaxLength(64)
            .HasConversion(hashedPassword => hashedPassword.Value,
                value => new HashedPassword(value))
            .HasColumnName("hashed_password");

        builder.Property(u => u.Subscription)
            .HasColumnName("subscription");
    }
}