using Microsoft.AspNetCore.Mvc.Testing;

namespace Mortein.IntegrationTests;

public partial class IndexTests(WebApplicationFactory<Startup> factory)
    : IClassFixture<WebApplicationFactory<Startup>>
{
    private readonly WebApplicationFactory<Startup> factory = factory;

    [Fact]
    public void DummyTest() { }
}
