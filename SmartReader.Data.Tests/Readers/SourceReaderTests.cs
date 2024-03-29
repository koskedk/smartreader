using Microsoft.Extensions.DependencyInjection;
using SmartReader.Core.Application.Interfaces;
using SmartReader.Core.Domain;

namespace SmartReader.Data.Tests.Readers;

[TestFixture]
public class SourceReaderTests
{
    private ISourceReader? _reader;
    
    [SetUp]
    public void Setup()
    {
        _reader = TestInitializer.ServiceProvider.GetRequiredService<ISourceReader>();
    }
    
    [Test]
    public async Task should_Get_Count()
    {
        var count =await _reader!.GetCount(GetExtract());
        Assert.That(count,Is.GreaterThan(0));
    }

    [Test]
    public void should_Create()
    {
        var reader = _reader!.GetReader(GetExtract());
        Assert.That(reader,Is.Not.Null);
        Assert.That(reader.IsClosed,Is.False);
        reader.Close();
        Assert.That(reader.IsClosed,Is.True);
    }

    private Extract GetExtract()
    {
        var ct = TestInitializer.ServiceProvider.GetRequiredService<SmartReaderDbContext>();
        return ct.Extracts.First(x => x.IsPriority);
    }
    
   

}