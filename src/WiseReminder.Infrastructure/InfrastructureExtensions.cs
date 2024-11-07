using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using WiseReminder.Domain.Abstractions;
using WiseReminder.Domain.Authors;
using WiseReminder.Domain.Categories;
using WiseReminder.Domain.Quotes;
using WiseReminder.Infrastructure.Data;
using WiseReminder.Infrastructure.Repositories;

namespace WiseReminder.Infrastructure;

public static class InfrastructureExtensions
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddDbContext<AppDbContext>(options =>
        {
            options.UseSqlServer(configuration.GetConnectionString("Database"));
        });

        services.AddScoped<IUnitOfWork>(factory => factory.GetRequiredService<AppDbContext>());

        services.AddScoped<ICategoryRepository, CategoryRepository>();
        services.AddScoped<IQuoteRepository, QuoteRepository>();
        services.AddScoped<IAuthorRepository, AuthorRepository>();

        return services;
    }

    public static void ApplyMigrations(this IApplicationBuilder app)
    {
        app.ApplicationServices.CreateScope().ServiceProvider.GetRequiredService<AppDbContext>().Database.Migrate();
    }
}