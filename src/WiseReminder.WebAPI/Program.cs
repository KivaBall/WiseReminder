//TODO: Create GlobalUsings.cs
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using WiseReminder.Application;
using WiseReminder.Infrastructure;
using WiseReminder.WebAPI.Middlewares;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//TODO: Move to another class 
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = "WiseReminder.com",
            ValidAudience = "WiseReminder.com",
            IssuerSigningKey =
                new SymmetricSecurityKey(
                    "ThisIsSpecialSecurityKeyThisIsSpecialSecurityKeyThisIsSpecialSecurityKeyThisIsSpecialSecurityKey"u8
                        .ToArray())
        };
    });

builder.Services.AddAuthorization();

builder.Services.AddApplicationServices();
builder.Services.AddInfrastructureServices(builder.Configuration);

var app = builder.Build();

app.ApplyMigrations();

//TODO: Replace
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

//TODO: Remove Middleware (why isn't stateless?)
app.UseMiddleware<JwtMiddleware>();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();