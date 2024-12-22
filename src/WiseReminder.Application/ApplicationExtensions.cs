namespace WiseReminder.Application;

public static class ApplicationExtensions
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddSerilog(config => config
            .WriteTo.Console()
            .WriteTo.Seq(configuration.GetConnectionString("Seq")));
        
        services.AddMediatR(config =>
        {
            config.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());

            config.AddOpenBehavior(typeof(SerilogPipeline<,>));
        });

        return services;
    }
}