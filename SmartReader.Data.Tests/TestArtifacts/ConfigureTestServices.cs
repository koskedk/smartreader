using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace SmartReader.Data.Tests.TestArtifacts;
public static class ConfigureTestServices
{
    public static IServiceCollection AddTestHttpClient(this IServiceCollection services)
    {
        var descriptor = new ServiceDescriptor(typeof(HttpClient), new TestHttpClient().GetHttpClient());
        services.Replace(descriptor);
        return services;
    }
}