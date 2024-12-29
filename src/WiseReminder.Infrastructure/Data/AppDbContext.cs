namespace WiseReminder.Infrastructure.Data;

public sealed class AppDbContext : DbContext, IUnitOfWork
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    public DbSet<Category> Categories { get; set; }
    public DbSet<Author> Authors { get; set; }
    public DbSet<Quote> Quotes { get; set; }
    public DbSet<User> Users { get; set; }

    public async Task<Result> SaveChangesAsync()
    {
        try
        {
            var saveAmount = await base.SaveChangesAsync();

            if (saveAmount <= 0)
            {
                return Result.Fail("Nothing was saved");
            }

            return Result.Ok();
        }
        catch
        {
            return Result.Fail("Something went wrong with database");
        }
    }

    public async Task<Result<TResult>> SaveChangesAsyncWithResult<TResult>(Func<TResult> entity)
    {
        try
        {
            var saveAmount = await base.SaveChangesAsync();

            if (saveAmount <= 0)
            {
                return Result.Fail("Nothing was saved");
            }

            return Result.Ok(entity.Invoke());
        }
        catch
        {
            return Result.Fail("Something went wrong with database");
        }
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
}