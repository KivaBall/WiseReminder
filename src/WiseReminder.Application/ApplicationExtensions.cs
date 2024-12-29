namespace WiseReminder.Application;

public static class ApplicationExtensions
{
    public static void AddApplicationServices(this IServiceCollection services,
        IConfiguration configuration)
    {
        if (configuration.GetConnectionString("SeqConnection") == "Default")
        {
            services.AddSerilog(config => config
                .WriteTo.Console());
        }
        else
        {
            services.AddSerilog(config => config
                .WriteTo.Console()
                .WriteTo.Seq(configuration.GetConnectionString("SeqConnection")!));
        }

        services.AddMediatR(config =>
        {
            config.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
            config.AddOpenBehavior(typeof(SerilogPipeline<,>));
        });
    }
}