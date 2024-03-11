using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SmartReader.Core.Application.Interfaces;
using SmartReader.Data.Readers;
using SmartReader.Data.Services;

namespace SmartReader.Data;

public static class DependencyInjection
{
    public static IServiceCollection AddDataServices(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("LiveConnection");
        var serverVersion = ServerVersion.AutoDetect(connectionString);
        
        services.AddMediatR(cfg => cfg
            .RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
        services.AddDbContext<SmartReaderDbContext>((sp, options) =>
        {
            options.UseMySql(connectionString,serverVersion);
        });

        services.AddScoped<ISmartReaderDbContext>(provider => provider.GetRequiredService<SmartReaderDbContext>());

        services.AddScoped<SmartReaderDbContextInitializer>();
        services.AddScoped<ISourceReader, SourceReader>();
        services.AddScoped<ISendService, SendService>();
       // services.AddHttpClient();
        
        return services;
    }
}