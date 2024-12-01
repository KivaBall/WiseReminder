namespace WiseReminder.Application;

public static class ApplicationExtensions
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        services.AddMediatR(config => config.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));

        services.AddScoped<ICategoryService, CategoryService>();
        services.AddScoped<IQuoteService, QuoteService>();
        services.AddScoped<IAuthorService, AuthorService>();

        return services;
    }
}