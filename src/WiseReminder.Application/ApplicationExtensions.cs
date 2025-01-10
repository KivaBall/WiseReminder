namespace WiseReminder.Application;

public static class ApplicationExtensions
{
    public static void AddApplicationServices(this IServiceCollection services,
        IConfiguration config)
    {
        services.AddLogging(config);

        services.AddCqrs();
    }

    private static void AddLogging(this IServiceCollection services, IConfiguration config)
    {
        services.AddSerilog(logger =>
        {
            logger.MinimumLevel.Is(Enum.Parse<LogEventLevel>(config["Logging:LogLevel:Default"]!));

            logger.WriteTo.Console();

            if (config.GetConnectionString("SeqConnection") != "Default")
            {
                logger.WriteTo.Seq(config.GetConnectionString("SeqConnection")!);
            }
        });
    }

    private static void AddCqrs(this IServiceCollection services)
    {
        services.AddMediatR(config =>
        {
            config.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());

            config.AddOpenBehavior(typeof(SerilogPipeline<,>));
        });
    }
}