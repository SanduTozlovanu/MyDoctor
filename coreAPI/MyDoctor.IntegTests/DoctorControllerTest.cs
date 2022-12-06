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
    public class DoctorControllerTest : IClassFixture<DatabaseFixture>
    {
        private readonly HttpClient _client;
        private DatabaseFixture databaseFixture;

        // Ctor is called for every test method
        public DoctorControllerTest(DatabaseFixture databaseFixture)
        {
            var app = new WebApplicationFactory<DoctorController>()
                .WithWebHostBuilder(builder => { });
            _client = app.CreateClient();
            this.databaseFixture = databaseFixture;
        }


        private void Init()
        {

        }
        [Fact, TestPriority(0)]
        public async Task TestCreateDoctor_NoMedicalRoom()
        {
            Init();
            string request = "https://localhost:7244/api/Doctor";
            var pDto = new CreateDoctorDto();
            pDto.UserDetails = new CreateUserDto
            {
                Email = "doctor@gmail.com",
                FirstName = "Ion",
                LastName = "Cutelaba",
                Password = "Test1234",
            };
            pDto.Speciality = "Chirurg";

            var content = new StringContent(JsonConvert.SerializeObject(pDto), Encoding.UTF8, "application/json");
            var res = await _client.PostAsync(request, content);
            var jsonString = await res.Content.ReadAsStringAsync();
            Assert.Equal(HttpStatusCode.NotFound, res.StatusCode);
            Assert.Equal(DoctorController.FreeMedicalRoomNotFoundError, jsonString);

        }

        [Fact, TestPriority(1)]
        public async Task TestCreateDoctor_Valid()
        {
            Init();
            string request2 = "https://localhost:7244/api/MedicalRoom";
            CreateMedicalRoomDto mdDto = new CreateMedicalRoomDto();
            mdDto.Adress = "Strada Cabinetului 20";

            var content2 = new StringContent(JsonConvert.SerializeObject(mdDto), Encoding.UTF8, "application/json");
            var res2 = await _client.PostAsync(request2, content2);
            Assert.Equal(HttpStatusCode.OK, res2.StatusCode);

            string request = "https://localhost:7244/api/Doctor";
            var pDto = new CreateDoctorDto();
            pDto.UserDetails = new CreateUserDto
            {
                Email = "doctor@gmail.com",
                FirstName = "Ion",
                LastName = "Cutelaba",
                Password = "Test1234",
            };
            pDto.Speciality = "Chirurg";

            var content = new StringContent(JsonConvert.SerializeObject(pDto), Encoding.UTF8, "application/json");
            var res = await _client.PostAsync(request, content);
            var jsonString = await res.Content.ReadAsStringAsync();
            var dto = JsonConvert.DeserializeObject<DisplayDoctorDto>(jsonString);
            Assert.True(dto.FirstName == pDto.UserDetails.FirstName);
            Assert.True(dto.Email == pDto.UserDetails.Email);
            Assert.True(dto.LastName == pDto.UserDetails.LastName);
            Assert.True(dto.Speciality == pDto.Speciality);
        }

        [Fact, TestPriority(2)]
        public async Task TestCreateDoctor_AlreadyUsedEmail()
        {

            // Given
            Init();

            // When
            string request = "https://localhost:7244/api/Doctor";
            var pDto = new CreateDoctorDto();
            pDto.UserDetails = new CreateUserDto
            {
                Email = "doctor@gmail.com",
                FirstName = "Test",
                LastName = "Test",
                Password = "Test1234",
            };
            pDto.Speciality = "Oculist";

            var content = new StringContent(JsonConvert.SerializeObject(pDto), Encoding.UTF8, "application/json");
            var res = await _client.PostAsync(request, content);
            var actualJsonString = await res.Content.ReadAsStringAsync();

            // Then
            Assert.Equal(HttpStatusCode.BadRequest, res.StatusCode);
            Assert.Equal(DoctorController.UsedEmailError, actualJsonString);
        }

        [Fact, TestPriority(3)]
        public async Task TestCreateDoctor_InvalidEmail()
        {

            // Given
            Init();

            // When
            string request = "https://localhost:7244/api/Doctor";
            var pDto = new CreateDoctorDto();
            pDto.UserDetails = new CreateUserDto
            {
                Email = "adresa",
                FirstName = "Test",
                LastName = "Test",
                Password = "Test1234",
            };
            pDto.Speciality = "Anarhist";

            var content = new StringContent(JsonConvert.SerializeObject(pDto), Encoding.UTF8, "application/json");
            var res = await _client.PostAsync(request, content);
            var actualJsonString = await res.Content.ReadAsStringAsync();

            // Then
            Assert.Equal(HttpStatusCode.BadRequest, res.StatusCode);
            Assert.Equal(DoctorController.InvalidEmailError, actualJsonString);
        }

    }
}