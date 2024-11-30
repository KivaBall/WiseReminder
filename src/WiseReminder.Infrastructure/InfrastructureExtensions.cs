//TODO: Add GlobalUsings.cs
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using WiseReminder.Application.Abstractions.JWT;
using WiseReminder.Domain.Abstractions;
using WiseReminder.Domain.Authors;
using WiseReminder.Domain.Categories;
using WiseReminder.Domain.Quotes;
using WiseReminder.Infrastructure.Data;
using WiseReminder.Infrastructure.JWT;
using WiseReminder.Infrastructure.Repositories;

namespace WiseReminder.Infrastructure;

public static class InfrastructureExtensions
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddDbContext<AppDbContext>(options =>
        {
            //TODO: Change name of database
            options.UseSqlServer(configuration.GetConnectionString("Database"));
        });

        services.AddScoped<IUnitOfWork>(factory => factory.GetRequiredService<AppDbContext>());

        //TODO: Create GenericRepository
        services.AddScoped<ICategoryRepository, CategoryRepository>();
        services.AddScoped<IQuoteRepository, QuoteRepository>();
        services.AddScoped<IAuthorRepository, AuthorRepository>();

        services.AddScoped<IJwtService, JwtService>();

        return services;
    }

    public static void ApplyMigrations(this IApplicationBuilder app)
    {
        //TODO: Replace all of this by .sql files
        using var scope = app.ApplicationServices.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
        var authorService = scope.ServiceProvider.GetRequiredService<IAuthorService>();
        var categoryService = scope.ServiceProvider.GetRequiredService<ICategoryService>();
        var quoteService = scope.ServiceProvider.GetRequiredService<IQuoteService>();

        context.Database.Migrate();

        if (context.Categories.Any() || context.Authors.Any() || context.Quotes.Any()) return;

        var categories = new[]
        {
            categoryService.CreateCategory(
                new CategoryName("Philosophy"),
                new CategoryDescription(
                    "This category features quotes that explore deep questions about existence, knowledge, ethics, and the nature of reality. It includes insights from renowned philosophers across different schools of thought.")),
            categoryService.CreateCategory(
                new CategoryName("Motivation"),
                new CategoryDescription(
                    "Quotes in this category aim to inspire action, perseverance, and personal growth. They encourage individuals to overcome challenges, stay determined, and pursue their goals with passion and resilience.")),
            categoryService.CreateCategory(
                new CategoryName("Love"),
                new CategoryDescription(
                    "The Love category offers quotes that reflect on the beauty, complexity, and power of love in all its forms—romantic, familial, and universal. These quotes inspire compassion, empathy, and connection."))
        };
        context.Categories.AddRange(categories);
        context.SaveChanges();

        //FAKE AUTHOR DATES
        var authors = new[]
        {
            authorService.CreateAuthor(new AuthorName("Aristotle"),
                new AuthorBiography("Aristotle was an Ancient Greek philosopher and polymath..."),
                new AuthorDateOfBirth(new DateOnly(384, 2, 26)),
                new AuthorDateOfDeath(new DateOnly(322, 1, 5))),
            authorService.CreateAuthor(new AuthorName("Confucius"),
                new AuthorBiography("Confucius, born Kong Qiu, was a Chinese philosopher..."),
                new AuthorDateOfBirth(new DateOnly(551, 7, 5)),
                new AuthorDateOfDeath(new DateOnly(479, 2, 1))),
            authorService.CreateAuthor(new AuthorName("Plato"),
                new AuthorBiography("Plato, born Aristocles, was an ancient Greek philosopher..."),
                new AuthorDateOfBirth(new DateOnly(428, 5, 2)),
                new AuthorDateOfDeath(new DateOnly(348, 2, 15))),
            authorService.CreateAuthor(new AuthorName("Friedrich Nietzsche"),
                new AuthorBiography("Friedrich Wilhelm Nietzsche was a German classical scholar..."),
                new AuthorDateOfBirth(new DateOnly(1844, 10, 15)),
                new AuthorDateOfDeath(new DateOnly(1900, 8, 25))),
            authorService.CreateAuthor(new AuthorName("Karl Marx"),
                new AuthorBiography("Karl Marx was a German-born philosopher, political theorist..."),
                new AuthorDateOfBirth(new DateOnly(1818, 5, 5)),
                new AuthorDateOfDeath(new DateOnly(1883, 3, 14)))
        };
        context.Authors.AddRange(authors);
        context.SaveChanges();

        var categoriesDict = context.Categories.ToDictionary(c => c.Name.Value);
        var authorsDict = context.Authors.ToDictionary(a => a.Name.Value);

        //FAKE QUOTES
        var quotes = new[]
        {
            quoteService.CreateQuote(new QuoteText("The unexamined life is not worth living."),
                authorsDict["Aristotle"].Id, authorsDict["Aristotle"], categoriesDict["Philosophy"].Id,
                categoriesDict["Philosophy"], new QuoteDate(new DateOnly(1953, 1, 1))),
            quoteService.CreateQuote(new QuoteText("He who learns but does not think, is lost..."),
                authorsDict["Confucius"].Id, authorsDict["Confucius"], categoriesDict["Philosophy"].Id,
                categoriesDict["Philosophy"], new QuoteDate(new DateOnly(1953, 1, 1))),
            quoteService.CreateQuote(new QuoteText("Wise men talk because they have something to say..."),
                authorsDict["Plato"].Id, authorsDict["Plato"], categoriesDict["Philosophy"].Id,
                categoriesDict["Philosophy"], new QuoteDate(new DateOnly(1953, 1, 1))),
            quoteService.CreateQuote(new QuoteText("That which does not kill us makes us stronger."),
                authorsDict["Friedrich Nietzsche"].Id, authorsDict["Friedrich Nietzsche"],
                categoriesDict["Philosophy"].Id, categoriesDict["Philosophy"], new QuoteDate(new DateOnly(1953, 1, 1))),
            quoteService.CreateQuote(new QuoteText("Workers of the world unite; you have nothing to lose..."),
                authorsDict["Karl Marx"].Id, authorsDict["Karl Marx"], categoriesDict["Philosophy"].Id,
                categoriesDict["Philosophy"], new QuoteDate(new DateOnly(1953, 1, 1))),
            quoteService.CreateQuote(new QuoteText("The only way to do great work is to love what you do."),
                authorsDict["Confucius"].Id, authorsDict["Confucius"], categoriesDict["Motivation"].Id,
                categoriesDict["Motivation"], new QuoteDate(new DateOnly(2023, 1, 1))),
            quoteService.CreateQuote(new QuoteText("Success is not the key to happiness..."), authorsDict["Plato"].Id,
                authorsDict["Plato"], categoriesDict["Motivation"].Id, categoriesDict["Motivation"],
                new QuoteDate(new DateOnly(2023, 1, 1))),
            quoteService.CreateQuote(new QuoteText("It does not matter how slowly you go..."),
                authorsDict["Aristotle"].Id, authorsDict["Aristotle"], categoriesDict["Motivation"].Id,
                categoriesDict["Motivation"], new QuoteDate(new DateOnly(2023, 1, 1))),
            quoteService.CreateQuote(new QuoteText("Love is composed of a single soul..."), authorsDict["Aristotle"].Id,
                authorsDict["Aristotle"], categoriesDict["Love"].Id, categoriesDict["Love"],
                new QuoteDate(new DateOnly(1953, 1, 1))),
            quoteService.CreateQuote(new QuoteText("To love and be loved is to feel the sun..."),
                authorsDict["Plato"].Id, authorsDict["Plato"], categoriesDict["Love"].Id, categoriesDict["Love"],
                new QuoteDate(new DateOnly(1953, 1, 1))),
            quoteService.CreateQuote(new QuoteText("The best thing to hold onto in life is each other."),
                authorsDict["Friedrich Nietzsche"].Id, authorsDict["Friedrich Nietzsche"], categoriesDict["Love"].Id,
                categoriesDict["Love"], new QuoteDate(new DateOnly(1886, 1, 1)))
        };
        context.Quotes.AddRange(quotes);
        context.SaveChanges();
    }
}