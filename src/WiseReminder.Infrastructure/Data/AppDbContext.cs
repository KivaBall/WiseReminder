namespace WiseReminder.Infrastructure.Data;

public sealed class AppDbContext(
    ILogger logger,
    DbContextOptions<AppDbContext> options)
    : DbContext(options), IUnitOfWork
{
    public DbSet<Category> Categories { get; set; }
    public DbSet<Author> Authors { get; set; }
    public DbSet<Quote> Quotes { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<Reaction> Reactions { get; set; }

    public new async Task<Result> SaveChangesAsync(CancellationToken cancellationToken)
    {
        return await ExecuteSaveChangesAsync(cancellationToken);
    }

    public async Task<Result<T>> SaveChangesAsync<T>(T entity, CancellationToken cancellationToken)
    {
        var result = await ExecuteSaveChangesAsync(cancellationToken);

        if (result.IsFailed)
        {
            return result;
        }

        return Result.Ok(entity);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }

    private async Task<Result> ExecuteSaveChangesAsync(CancellationToken cancellationToken)
    {
        try
        {
            if (!ChangeTracker.HasChanges())
            {
                return DbErrors.DetectedNoChanges;
            }

            await base.SaveChangesAsync(cancellationToken);

            return Result.Ok();
        }
        catch (Exception ex)
        {
            logger.Error(ex, "An error occured during SaveChangesAsync");

            return DbErrors.DatabaseError;
        }
    }
}