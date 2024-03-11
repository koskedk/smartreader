using Dapper;
using Microsoft.EntityFrameworkCore;
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
    public async Task should_Initialize_History()
    {
        await _reader!.InitializeHistory(CancellationToken.None);
        Assert.That(GetHistory(), Is.GreaterThan(0));
    }
    
    [Test]
    public async Task should_Clear_History()
    {
        await _reader!.ClearHistory(CancellationToken.None);
        Assert.That(GetHistory(), Is.EqualTo(0));
    }

    
    
    [Test]
    public async Task should_Save_Error_History()
    {
        await _reader!.InitializeHistory(CancellationToken.None);
        var exId = GetExtract().Id;
        
        await _reader!.UpdateStatusHistory(exId,"Error");
        Assert.That(GetHistory(exId).Status, Is.EqualTo("Error"));
    }
    
    [Test]
    public async Task should_Save_Sent_History()
    {
        await _reader!.InitializeHistory(CancellationToken.None);
        var exId = GetExtract().Id;
        
        await _reader!.UpdateSentHistory(exId,3);
        Assert.That(GetHistory(exId).Sent, Is.EqualTo(3));
    }
    
    [Test]
    public async Task should_Save_Load_History()
    {   
        await _reader!.InitializeHistory(CancellationToken.None);
        var exId = GetExtract().Id;
        
        await _reader!.UpdateLoadHistory(exId,4);
        Assert.That(GetHistory(exId).Loaded, Is.EqualTo(4));
    }
    
    [Test]
    public async Task should_Get_Count()
    {
        var count= await _reader!.GetCount(GetExtract(),CancellationToken.None);
        Assert.That(count, Is.GreaterThanOrEqualTo(0));
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
    
    private int GetHistory()
    {
        var ct = TestInitializer.ServiceProvider.GetRequiredService<SmartReaderDbContext>();
        return ct.ExtractHistories.AsNoTracking().Count();
    }
    private ExtractHistory GetHistory(int extractId)
    {
        var ct = TestInitializer.ServiceProvider.GetRequiredService<SmartReaderDbContext>();
        var exh=ct.Database.GetDbConnection().Query<ExtractHistory>($"select * from {nameof(SmartReaderDbContext.ExtractHistories)}").First(x => x.ExtractId == extractId);
        return exh;
    }
}