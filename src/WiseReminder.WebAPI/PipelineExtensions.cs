namespace WiseReminder.WebAPI;

public static class PipelineExtensions
{
    public static async Task ApplyDbConfiguration(this WebApplication app)
    {
        if (app.Configuration["AllowMigrations"] == "true")
        {
            await app.ApplyMigrations();
        }

        if (app.Configuration["AllowSeeding"] == "true")
        {
            await app.ApplySeeding();
        }
    }

    public static void BuildPipeline(this WebApplication app)
    {
        if (app.Configuration["AllowScalar"] == "true")
        {
            app.MapOpenApi();
            app.MapScalarApiReference(opt =>
            {
                opt.WithTheme(ScalarTheme.Kepler);
                opt.WithOperationSorter(OperationSorter.Method);
                opt.WithDefaultHttpClient(ScalarTarget.CSharp, ScalarClient.HttpClient);
                opt.WithClientButton(false);
                opt.WithForceThemeMode(ThemeMode.Dark);
            });
        }

        app.UseHttpsRedirection();

        app.UseAuthentication();

        app.UseAuthorization();

        app.MapControllers();
    }
}