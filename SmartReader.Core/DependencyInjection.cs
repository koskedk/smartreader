using System.Reflection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SmartReader.Core.Application.Dtos;

namespace SmartReader.Core;

public static class DependencyInjection
{
    public static IServiceCollection AddCoreServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddMediatR(cfg => cfg
            .RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
        services.AddAutoMapper(typeof(ReaderProfile));
        return services;
    }
}