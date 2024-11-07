using System.Reflection;
using Microsoft.EntityFrameworkCore;
using WiseReminder.Domain.Abstractions;
using WiseReminder.Domain.Authors;
using WiseReminder.Domain.Categories;
using WiseReminder.Domain.Quotes;

namespace WiseReminder.Infrastructure.Data;

public sealed class AppDbContext : DbContext, IUnitOfWork
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    public DbSet<Category> Categories { get; set; }
    public DbSet<Author> Authors { get; set; }
    public DbSet<Quote> Quotes { get; set; }

    public async Task<bool> SaveChangesAsync()
    {
        return await base.SaveChangesAsync() > 0;
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        base.OnModelCreating(modelBuilder);
    }
}