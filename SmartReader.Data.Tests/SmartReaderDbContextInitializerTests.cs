using Microsoft.Extensions.DependencyInjection;

namespace SmartReader.Data.Tests;

[TestFixture]
public class SmartReaderDbContextInitializerTests
{
    private SmartReaderDbContextInitializer? _initializer;
    [SetUp]
    public void Setup()
    {
        _initializer = TestInitializer.ServiceProvider.GetRequiredService<SmartReaderDbContextInitializer>();
    }

    [Test]
    public async Task should_seed()
    {
        await _initializer!.SeedAsync();
        var ct = TestInitializer.ServiceProvider.GetRequiredService<SmartReaderDbContext>();
        Assert.That(ct.Registries.Any);
        Assert.That(ct.Extracts.Any);
        Assert.That(ct.Configs.Any);
    }
}