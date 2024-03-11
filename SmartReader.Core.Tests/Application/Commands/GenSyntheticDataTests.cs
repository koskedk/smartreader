using MediatR;
using Microsoft.Extensions.DependencyInjection;
using SmartReader.Core.Application.Commands;

namespace SmartReader.Core.Tests.Application.Commands;

[TestFixture]
public class GenSyntheticDataTests
{
    private IMediator _mediatr;

    [SetUp]
    public void SetUp()
    {
        _mediatr = TestInitializer.ServiceProvider.GetRequiredService<IMediator>();
    }

    [TestCase(22222,"Site 22222",2,null,null)]
    [TestCase(33333,"Site 33333",3,10,20)]
    [TestCase(44444,"Site 44444",6,null,10)]
    public async Task should_Gen(int site,string fac,int count,int? visits,int? pharms)
    {
        var res= await _mediatr.Send(new GenSyntheticData(site,fac,count,visits,pharms));
        Assert.That(res.IsSuccess);
        Assert.That(res.Value.Any);
    }
}