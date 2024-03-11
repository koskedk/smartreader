using System.Net;
using RichardSzalay.MockHttp;

namespace SmartReader.Core.Tests.TestArtifacts;

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