using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using MyDoctor.API.DTOs;
using MyDoctor.API.Helpers;
using MyDoctor.Domain.Models;
using MyDoctorApp.Infrastructure;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json.Nodes;

namespace TestProject1
{
    public class IntegrationTests : IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly HttpClient _client;
        private readonly DatabaseContext dbContext;

        public IntegrationTests(WebApplicationFactory<Program> factory)
        {
            _client = factory.CreateClient();
            dbContext = CreateDbContext();
        }

        private DatabaseContext CreateDbContext()
        {
            var context = new DatabaseContext();
            DatabaseContext.DatabaseName = "Teste.db";
            context.Database.EnsureCreated();

            return context;
        }

        private void Init()
        {
            // Create 1

            // Create 2

        }

        [Fact]
        public async Task PatientsController()
        {

            // Given
            Init();
            

            // When
            string request = "https://localhost:7244/api/Patients";
            var response = await _client.GetAsync(request);
            //CreatePatientDto pDto = new CreatePatientDto();
            //pDto.Age = 10;
            //pDto.UserDetails = new CreateUserDto();
            //pDto.UserDetails.Email = "Test@gmail.com";
            //pDto.UserDetails.FirstName = "Test";
            //pDto.UserDetails.LastName = "Integration";

            //var res = await _client.PostAsJsonAsync(request, pDto);

            var data = JObject.FromObject(new
            {
                UserDetails = new {
                    Email = "Test@gmail.com",
                    Password = "Test1234",
                    FirstName = "Test",
                    LastName = "Integration"
                },
                Age = 10
            });

            var content = new StringContent(JsonConvert.SerializeObject(data), Encoding.UTF8, "application/json");
            var res = await _client.PostAsync(request, content);
            var cont = res.Content.ReadAsStringAsync();

            //var pat = new Patient("str", "str", "str", "str", 20);
            //var jwt = JwtManager.GenerateToken(pat);
            //bool valid = JwtManager.ValidateCurrentToken(jwt);
            var r = res;

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