using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.VisualStudio.TestPlatform.TestHost;

namespace TestProject1
{
    public class IntegrationTests:IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly HttpClient _client;

        public IntegrationTests(WebApplicationFactory<Program> factory)
        {
            _client = factory.CreateClient();
        }

        [Fact]
        public async Task PatientsController()
        {
            string request = "/";
            var response = await _client.GetAsync(request);
        }
    }
}