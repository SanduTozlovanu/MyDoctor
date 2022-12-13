namespace MyDoctor.Tests.Helpers
{
    [TestCaseOrderer("MyDoctor.Tests.Orderers.PriorityOrderer", "MyDoctor.Tests")]
    public class BaseControllerTest<T> : IClassFixture<CustomWebApplicationFactory<Program>> where T: class
    {
        protected readonly HttpClient HttpClient;
        protected CustomWebApplicationFactory<Program> Factory = new CustomWebApplicationFactory<Program>();

        // Ctor is called for every test method
        public BaseControllerTest(CustomWebApplicationFactory<Program> factory)
        {
            Factory = factory;
            HttpClient = factory.CreateClient();
        }
    }
}