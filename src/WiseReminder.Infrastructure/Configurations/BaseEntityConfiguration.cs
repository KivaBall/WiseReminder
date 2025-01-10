namespace WiseReminder.Infrastructure.Configurations;

public abstract class BaseEntityConfiguration<TEntity> : IEntityTypeConfiguration<TEntity>
    where TEntity : Entity<TEntity>
{
    public virtual void Configure(EntityTypeBuilder<TEntity> builder)
    {
        builder.Property(b => b.AddedAt)
            .HasColumnName("added_at");

        builder.Property(b => b.IsDeleted)
            .HasColumnName("is_deleted");

        builder.Property(b => b.DeletedAt)
            .HasColumnName("deleted_at");

        builder.HasQueryFilter(b => b.IsDeleted == false);
    }

    protected void ApplyPrimaryKey(EntityTypeBuilder<TEntity> builder)
    {
        builder.Property(b => b.Id)
            .HasColumnName("id");

        builder.HasKey(b => b.Id);
    }
}