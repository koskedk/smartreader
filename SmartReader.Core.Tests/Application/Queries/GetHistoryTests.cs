using MediatR;
using Microsoft.Extensions.DependencyInjection;
using SmartReader.Core.Application.Commands;
using SmartReader.Core.Application.Queries;

namespace SmartReader.Core.Tests.Application.Queries;

[TestFixture]
public class GetHistoryTests
{
    private IMediator _mediatr;

    [SetUp]
    public void SetUp()
    {
        _mediatr = TestInitializer.ServiceProvider.GetRequiredService<IMediator>();
    }

    [Test]
    public async Task Should_Get()
    {
        await _mediatr.Send(new ScanExtracts());
        
        var res= await _mediatr.Send(new GetHistory());
        Assert.That(res.IsSuccess);
        Assert.That(res.Value.Any);
    }
}