using MediatR;
using Microsoft.Extensions.DependencyInjection;
using SmartReader.Core.Application.Commands;

namespace SmartReader.Core.Tests.Application.Commands;

[TestFixture]
public class SendExtractsTests
{
    private IMediator _mediatr;

    [SetUp]
    public void SetUp()
    {
        _mediatr = TestInitializer.ServiceProvider.GetRequiredService<IMediator>();
    }

    [TestCase(1)]
    public async Task Should_Send(int registry)
    {
        var res= await _mediatr.Send(new SendExtracts(registry));
        Assert.That(res.IsSuccess);
    }
}