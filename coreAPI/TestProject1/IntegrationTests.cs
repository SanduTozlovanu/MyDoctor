using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using MyDoctor.API.Helpers;
using MyDoctor.Domain.Models;
using MyDoctorApp.Infrastructure;
using System.Net.Http.Json;

namespace TestProject1
{
    public class IntegrationTests:IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly HttpClient _client;

        public IntegrationTests(WebApplicationFactory<Program> factory)
        {
            _client = factory.CreateClient();
        }

        private DatabaseContext CreateDbContext()
        {
            var context = new DatabaseContext();
            context.Database.EnsureCreated();

            context.Database.EnsureCreated();

            return context;
        }

        [Fact]
        public async Task PatientsController()
        {

            // Given
            var dbContext = CreateDbContext();

            // When
            string request = "https://localhost:7244/api/Appointment";
            var response = await _client.GetAsync(request);
            //var res = await _client.PostAsJsonAsync
            var pat = new Patient("str", "str", "str", "str", 20);
            var jwt = JwtManager.GenerateToken(pat);
            bool valid = JwtManager.ValidateCurrentToken(jwt);
            var r = response;

            // Then
            //Assert.Empty(result);
            //var options = new DbContextOptionsBuilder<DatabaseContext>()
            //    .UseSqlite("Data Source = MyDoctorApp.db")
            //    .Options;

            //var context = new DatabaseContext();
            //context.Database.EnsureCreated();

            //string request = "https://localhost:7244/api/Appointment";
            //var response = await _client.GetAsync(request);
            //var r = response;
        }
    }
}