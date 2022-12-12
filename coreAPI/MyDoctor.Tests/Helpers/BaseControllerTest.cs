using Microsoft.AspNetCore.Mvc.Testing;
using MyDoctor.API.Controllers;

namespace MyDoctor.Tests.Helpers
{
    [TestCaseOrderer("MyDoctor.Tests.Orderers.PriorityOrderer", "MyDoctor.Tests")]
    public class BaseControllerTest<T> : IClassFixture<DatabaseFixtureGeneric<T>> where T: class
    {
        protected readonly HttpClient _client;
        protected DatabaseFixtureGeneric<T> databaseFixture = new DatabaseFixtureGeneric<T>();

        // Ctor is called for every test method
        public BaseControllerTest()
        {
            var app = new WebApplicationFactory<T>()
                .WithWebHostBuilder(builder => { });
            _client = app.CreateClient();
        }
    }
}