using Microsoft.AspNetCore.Mvc.Testing;
using MyDoctor.API.Controllers;
using MyDoctor.API.DTOs;
using MyDoctor.Tests.Helpers;
using MyDoctor.Tests.Orderers;
using Newtonsoft.Json;
using System.Net;
using System.Text;

namespace MyDoctor.Tests.IntegTests
{
    [TestCaseOrderer("MyDoctor.Tests.Orderers.PriorityOrderer", "MyDoctor.Tests")]
    public class PatientControllerTest : IClassFixture<DatabaseFixture>
    {
        private readonly HttpClient _client;
        private DatabaseFixture databaseFixture;

        // Ctor is called for every test method
        public PatientControllerTest(DatabaseFixture databaseFixture)
        {
            var app = new WebApplicationFactory<PatientController>()
                .WithWebHostBuilder(builder => { });
            _client = app.CreateClient();
            this.databaseFixture = databaseFixture;
        }


        private void Init()
        {

        }

        [Fact, TestPriority(0)]
        public async Task TestCreatePacient_Valid()
        {

            // Given
            Init();

            // When
            string request = "https://localhost:7244/api/Patient";
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
            var jsonString = await res.Content.ReadAsStringAsync();
            var dto = JsonConvert.DeserializeObject<DisplayPatientDto>(jsonString);
            Assert.True(dto.FirstName == pDto.UserDetails.FirstName);
            Assert.True(dto.Email == pDto.UserDetails.Email);
            Assert.True(dto.LastName == pDto.UserDetails.LastName);
            Assert.True(dto.Age == pDto.Age);

            // Then
            Assert.Equal(HttpStatusCode.OK, res.StatusCode);
        }

        [Fact, TestPriority(1)]
        public async Task TestCreatePacient_AlreadyUsedEmail()
        {

            // Given
            Init();

            // When
            string request = "https://localhost:7244/api/Patient";
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

            // Then
            Assert.Equal(HttpStatusCode.BadRequest, res.StatusCode);
            Assert.Equal(PatientController.UsedEmailError, actualJsonString);
        }

        [Fact, TestPriority(2)]
        public async Task TestCreatePacient_BigAge()
        {

            // Given
            Init();

            // When
            string request = "https://localhost:7244/api/Patient";
            var pDto = new CreatePatientDto();
            pDto.UserDetails = new CreateUserDto
            {
                Email = "troller@gmail.com",
                FirstName = "Test",
                LastName = "Test",
                Password = "Test1234",
            };
            pDto.Age = 150;

            var content = new StringContent(JsonConvert.SerializeObject(pDto), Encoding.UTF8, "application/json");
            var res = await _client.PostAsync(request, content);
            var actualJsonString = await res.Content.ReadAsStringAsync();

            // Then
            Assert.Equal(HttpStatusCode.BadRequest, res.StatusCode);
            Assert.Equal(PatientController.BigAgeError, actualJsonString);
        }

        [Fact, TestPriority(3)]
        public async Task TestCreatePacient_InvalidEmail()
        {

            // Given
            Init();

            // When
            string request = "https://localhost:7244/api/Patient";
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

            // Then
            Assert.Equal(HttpStatusCode.BadRequest, res.StatusCode);
            Assert.Equal(PatientController.InvalidEmailError, actualJsonString);
        }

    }
}
