using Microsoft.AspNetCore.Mvc.Testing;
using MyDoctorApp.Infrastructure;
[assembly: CollectionBehavior(DisableTestParallelization = true)]
namespace MyDoctor.IntegTests
{
    public abstract class AIntegrationTest : IClassFixture<WebApplicationFactory<Program>>, IDisposable
    {
        protected readonly HttpClient _client;
        protected readonly DatabaseContext dbContext;

        public AIntegrationTest(WebApplicationFactory<Program> factory)
        {
            _client = factory.CreateClient();
            dbContext = new DatabaseContext();
            DatabaseContext.DatabaseName = "Tests.db";
            dbContext.Database.EnsureCreated();
        }

        public void Dispose()
        {
            dbContext.Database.EnsureDeleted();
        }

    }
}
