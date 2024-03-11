using System.Reflection;
using Dapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Serilog;
using SmartReader.Core;
using SmartReader.Data.Tests.TestArtifacts;

namespace SmartReader.Data.Tests;

[SetUpFixture]
public class TestInitializer
{
    public static IServiceProvider ServiceProvider;
    public static IConfiguration Configuration;

    [OneTimeSetUp]
    public void Init()
    {
        Log.Logger = new LoggerConfiguration()
            .MinimumLevel.Information()
            .WriteTo.Console()
            .CreateLogger();
        
        var config =Configuration= new ConfigurationBuilder()
            .AddJsonFile("appsettings.Test.json", optional: false, reloadOnChange: true)
            .AddEnvironmentVariables()
            .Build();
        var services = new ServiceCollection();
        services.AddMediatR(cfg => cfg
            .RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
        services.AddDataServices(config);
        services.AddCoreServices(config);
        services.AddTestHttpClient();
        ServiceProvider = services.BuildServiceProvider();
        InitDb();
        ClearDb();
        AddTestData();
    }

    private static void InitDb()
    { 
        var initializer = ServiceProvider.GetRequiredService<SmartReaderDbContextInitializer>();
       initializer.InitialiseAsync().Wait();
       initializer.SeedAsync().Wait();
    }
    
    private static void  ClearDb()
    {
        var ct = ServiceProvider.GetRequiredService<SmartReaderDbContext>();
        ct.Database.GetDbConnection().Execute($"truncate table {nameof(ct.Patients)}");
        ct.Database.GetDbConnection().Execute($"truncate table {nameof(ct.PatientVisits)}");
        ct.Database.GetDbConnection().Execute($"truncate table {nameof(ct.PatientPharmacies)}");
    }
    
    private  static void AddTestData()
    {
        var patients = TestData.GetTestPatients();
        var visits = TestData.GetTestVisits(patients);
        var pharmacies = TestData.GetTestPharmacies(patients);
        var ct = TestInitializer.ServiceProvider.GetRequiredService<SmartReaderDbContext>();
        ct.AddRange(patients);
        ct.AddRange(visits);
        ct.AddRange(pharmacies);
        ct.SaveChanges();
    }
}