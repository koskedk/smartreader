using System.Net;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using RichardSzalay.MockHttp;

namespace SmartReader.Data.Tests.TestArtifacts;

public class TestHttpClient
{
    public HttpClient GetHttpClient()
    {
        var mockHttp = new MockHttpMessageHandler();

        mockHttp
            .When(HttpMethod.Post, "https://localhost:7267/api/Ct/*")
            .Respond(HttpStatusCode.OK);

        return mockHttp.ToHttpClient();
    }
}