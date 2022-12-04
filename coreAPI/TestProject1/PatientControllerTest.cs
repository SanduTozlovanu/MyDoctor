using Microsoft.AspNetCore.Mvc.Testing;
using MyDoctor.API.Controllers;
using MyDoctor.API.DTOs;
using MyDoctor.IntegTests.Helpers;
using MyDoctor.IntegTests.Orderers;
using Newtonsoft.Json;
using System.Net;
using System.Text;

namespace MyDoctor.IntegTests
{
    [TestCaseOrderer("MyDoctor.IntegTests.Orderers.PriorityOrderer", "MyDoctor.IntegTests")]
    public class PatientControllerTest : IClassFixture<DatabaseFixture>
    {
        private readonly HttpClient _client;
        DatabaseFixture databaseFixture;

        // Ctor is called for every test method
        public PatientControllerTest(DatabaseFixture databaseFixture)
        {
            var app = new WebApplicationFactory<PatientsController>()
                .WithWebHostBuilder(builder => { });
            _client = app.CreateClient();
            this.databaseFixture = databaseFixture;
        }


        private void Init()
        {

        }

        [Fact, TestPriority(0)]
        public async Task CreatePacient_Valid()
        {

            // Given
            Init();

            // When
            string request = "https://localhost:7244/api/Patients";
            var pDto = new CreatePatientDto();
            pDto.UserDetails = new CreateUserDto
            {
                Email = "adresa@gmail.com",
                FirstName = "Test",
                LastName = "Test",
                Password = "Test1234",
            };
            pDto.Age = 15;

            var content = new StringContent(JsonConvert.SerializeObject(pDto), Encoding.UTF8, "application/json");
            var res = await _client.PostAsync(request, content);

            // Then
            Assert.Equal(HttpStatusCode.OK, res.StatusCode);
        }

        [Fact, TestPriority(1)]
        public async Task CreatePacient_AlreadyUsedEmail()
        {

            // Given
            Init();

            // When
            string request = "https://localhost:7244/api/Patients";
            var pDto = new CreatePatientDto();
            pDto.UserDetails = new CreateUserDto
            {
                Email = "adresa@gmail.com",
                FirstName = "Test",
                LastName = "Test",
                Password = "Test1234",
            };
            pDto.Age = 15;

            var content = new StringContent(JsonConvert.SerializeObject(pDto), Encoding.UTF8, "application/json");
            var res = await _client.PostAsync(request, content);
            var actualJsonString = await res.Content.ReadAsStringAsync();
            var expectedJsonString = "The email is already used!";

            // Then
            Assert.Equal(HttpStatusCode.BadRequest, res.StatusCode);
            Assert.Equal(actualJsonString, expectedJsonString);
        }

        [Fact, TestPriority(2)]
        public async Task CreatePacient_InvalidEmail()
        {

            // Given
            Init();

            // When
            string request = "https://localhost:7244/api/Patients";
            var pDto = new CreatePatientDto();
            pDto.UserDetails = new CreateUserDto
            {
                Email = "adresa",
                FirstName = "Test",
                LastName = "Test",
                Password = "Test1234",
            };
            pDto.Age = 15;

            var content = new StringContent(JsonConvert.SerializeObject(pDto), Encoding.UTF8, "application/json");
            var res = await _client.PostAsync(request, content);
            var actualJsonString = await res.Content.ReadAsStringAsync();
            var expectedJsonString = "The email is invalid!";

            // Then
            Assert.Equal(HttpStatusCode.BadRequest, res.StatusCode);
            Assert.Equal(actualJsonString, expectedJsonString);
        }

    }
}
