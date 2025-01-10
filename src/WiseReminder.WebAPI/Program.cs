var builder = WebApplication.CreateBuilder(args);

builder.Services.AddApplicationServices(builder.Configuration);

builder.Services.AddInfrastructureServices(builder.Configuration);

builder.Services.AddPresentationServices(builder.Configuration);

var app = builder.Build();

await app.ApplyDbConfiguration();

app.BuildPipeline();

await app.RunAsync();

public abstract partial class Program;