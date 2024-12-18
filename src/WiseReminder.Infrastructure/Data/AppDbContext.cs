namespace WiseReminder.Infrastructure.Data;

public sealed class AppDbContext : DbContext, IUnitOfWork
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    public DbSet<Category> Categories { get; set; }
    public DbSet<Author> Authors { get; set; }
    public DbSet<Quote> Quotes { get; set; }

    public async Task<Result> SaveChangesAsync()
    {
        if (await base.SaveChangesAsync() <= 0)
        {
            return Result.Fail("Database operation failed");
        }

        return Result.Ok();
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
}