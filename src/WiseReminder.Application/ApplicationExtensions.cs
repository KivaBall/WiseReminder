using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using WiseReminder.Domain.Authors;
using WiseReminder.Domain.Categories;
using WiseReminder.Domain.Quotes;

namespace WiseReminder.Application;

public static class ApplicationExtensions
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        //TODO: Replace AutoMapper (obsolete)
        services.AddAutoMapper(Assembly.GetExecutingAssembly());

        services.AddMediatR(config => config.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));

        services.AddScoped<ICategoryService, CategoryService>();
        services.AddScoped<IQuoteService, QuoteService>();
        services.AddScoped<IAuthorService, AuthorService>();

        return services;
    }
}