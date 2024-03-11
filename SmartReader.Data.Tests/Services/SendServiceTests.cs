using Microsoft.Extensions.DependencyInjection;
using SmartReader.Core.Application.Interfaces;
using SmartReader.Core.Domain;
using SmartReader.Core.Domain.Ct;
using SmartReader.Data.Tests.TestArtifacts;

namespace SmartReader.Data.Tests.Services;

[TestFixture]
public class SendServiceTests
{
    private ISendService? _sendService;
    
    [SetUp]
    public void Setup()
    {
        _sendService = TestInitializer.ServiceProvider.GetRequiredService<ISendService>();
    }
    
    [TestCase(1,"Patient")]
    [TestCase(1,"Pharmacy")]
    [TestCase(1,"Visit")]
    public async Task should_Send(int registry, string extract)
    {
        if (extract == "Patient")
        {
            var result = await _sendService!.Send<Patient>(GetRegistry(registry), GetExtract(extract), 10,CancellationToken.None);
            Assert.That(result.IsSuccess);
        }
        
        if (extract == "Pharmacy")
        {
            var result = await _sendService!.Send<PatientPharmacy>(GetRegistry(registry), GetExtract(extract), 10,CancellationToken.None);
            Assert.That(result.IsSuccess);
        }
        
        if (extract == "Visit")
        {
            var result = await _sendService!.Send<PatientVisit>(GetRegistry(registry), GetExtract(extract), 10,CancellationToken.None);
            Assert.That(result.IsSuccess);
        }
    }

    private Registry GetRegistry(int id)
    {
        var ct = TestInitializer.ServiceProvider.GetRequiredService<SmartReaderDbContext>();
        return ct.Registries.First(x=>x.Id==id);
    }
    private Extract GetExtract(string name)
    {
        var ct = TestInitializer.ServiceProvider.GetRequiredService<SmartReaderDbContext>();
        return ct.Extracts.First(x => x.Name.ToLower()==name.ToLower());
    }
}