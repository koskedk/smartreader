using Microsoft.EntityFrameworkCore;
using SmartReader.Central.Application.Interfaces;
using SmartReader.Central.Data;

namespace SmartReader.Central.Configuration;

public static class DependencyInjection
{
    public static IServiceCollection AddDatabase(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("LiveConnection");

        
        services.AddDbContext<SmartCentralDbContext>((sp, options) =>
        {
            options.UseSqlServer(connectionString);
        });

        services.AddScoped<ISmartCentralDbContext>(provider => provider.GetRequiredService<SmartCentralDbContext>());

        services.AddScoped<SmartCentralDbContextInitializer>();
        
        return services;
    }
    
    public static async Task InitialiseDatabaseAsync(this WebApplication app)
    {
        using var scope = app.Services.CreateScope();

        var initializer = scope.ServiceProvider.GetRequiredService<SmartCentralDbContextInitializer>();

        await initializer.InitialiseAsync();

        await initializer.SeedAsync();
    }
}