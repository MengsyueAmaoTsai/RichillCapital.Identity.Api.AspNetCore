using Microsoft.OpenApi.Models;

using RichillCapital.Core.Features;
using RichillCapital.Identity.Api.Middlewares;
using RichillCapital.Infrastructure.Persistence;

using Serilog;

namespace RichillCapital.Identity.Api;

public static class WebApplicationExtensions
{
    public static WebApplicationBuilder ConfigureServices(this WebApplicationBuilder builder)
    {
        builder.Host.UseSerilog((context, configuration) =>
            configuration.ReadFrom.Configuration(builder.Configuration));

        builder.Services.AddFeatures();
        builder.Services.AddPersistence();
        builder.Services.AddEndpoints();
        builder.Services.AddMiddlewares();

        return builder;
    }

    public static Task<WebApplication> ConfigurePipeline(this WebApplication app)
    {
        app.UseMiddleware<RequestContextLoggingMiddleware>();
        app.UseSerilogRequestLogging();

        app
            .UseSwagger()
            .UseSwaggerUI();

        app.UseMiddleware<GlobalExceptionHandlingMiddleware>();
        app.MapControllers();

        app.UseSeedData();

        // await app.UseDataFeeds();
        // await app.UseBrokerages();
        return Task.FromResult(app);
    }

    // public static async Task<WebApplication> UseDataFeeds(this WebApplication app)
    // {
    //     using var scope = app.Services.CreateScope();
    //     var dataFeedService = scope.ServiceProvider.GetRequiredService<IDataFeedService>();
    //     await dataFeedService.InitializeAsync();
    //     return app;
    // }

    // public static async Task<WebApplication> UseBrokerages(this WebApplication app)
    // {
    //     using var scope = app.Services.CreateScope();
    //     var brokerageService = scope.ServiceProvider.GetRequiredService<IBrokerageService>();
    //     await brokerageService.InitializeAsync();
    //     return app;
    // }
    public static WebApplication UseSeedData(this WebApplication app)
    {
        using var scope = app.Services.CreateScope();
        var services = scope.ServiceProvider;
        var logger = services.GetRequiredService<ILogger<Program>>();

        try
        {
            var context = services.GetRequiredService<MsSqlDbContext>();
            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();
            Seeds.Initialize(services);

            logger.LogInformation("Seed populated successfully.");
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "An error occurred seeding the database. {exceptionMessage}", ex.Message);
        }

        return app;
    }

    private static IServiceCollection AddEndpoints(this IServiceCollection services)
    {
        services.AddCors(options =>
        {
            options.AddPolicy("default", builder => builder
                .AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader());
        });

        services.AddControllers();

        services.AddProblemDetails();

        services
            .AddEndpointsApiExplorer()
            .AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo { Title = "Identity.Api", Version = "v1" });
                options.EnableAnnotations();
            });

        return services;
    }

    private static IServiceCollection AddMiddlewares(this IServiceCollection services)
    {
        services.AddTransient<GlobalExceptionHandlingMiddleware>();
        services.AddTransient<RequestContextLoggingMiddleware>();

        return services;
    }
}