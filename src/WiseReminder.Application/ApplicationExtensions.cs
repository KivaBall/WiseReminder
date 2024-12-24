namespace WiseReminder.Application;

public static class ApplicationExtensions
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services,
        IConfiguration configuration)
    {
        Log.Logger = new LoggerConfiguration()
            .WriteTo.Console()
            .WriteTo.Seq(configuration.GetConnectionString("SeqConnection") ??
                         throw new NullReferenceException())
            .CreateLogger();

        services.AddSerilog(config => config
            .WriteTo.Console()
            .WriteTo.Seq(configuration.GetConnectionString("SeqConnection") ??
                         throw new NullReferenceException()));

        services.AddMediatR(config =>
        {
            config.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());

            config.AddOpenBehavior(typeof(SerilogPipeline<,>));
        });

        return services;
    }
}