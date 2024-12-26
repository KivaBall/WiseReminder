namespace WiseReminder.Infrastructure.Configurations;

public sealed class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.HasKey(u => u.Id);
        builder.Property(u => u.AddedAt);
        
        builder.HasQueryFilter(u => u.DeletedAt == null);
        builder.HasQueryFilter(u => u.IsDeleted == false);
        
        builder.Property(u => u.Username)
            .HasMaxLength(64)
            .HasConversion(username => username.Value, value => new Username(value));
        
        builder.Property(u => u.Login)
            .HasMaxLength(64)
            .HasConversion(login => login.Value, value => new Login(value));
        
        builder.Property(u => u.HashedPassword)
            .HasMaxLength(64)
            .HasConversion(hashedPassword => hashedPassword.Value, value => new HashedPassword(value));
    }
}