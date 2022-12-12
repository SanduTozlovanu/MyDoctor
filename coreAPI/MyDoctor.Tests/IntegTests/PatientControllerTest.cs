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
    public class PatientControllerTest : BaseControllerTest<PatientController>
    {

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
            var pDto = new CreatePatientDto(new CreateUserDto("adresa@gmail.com", "Test", "Test", "Test1234"), 15);

            var content = new StringContent(JsonConvert.SerializeObject(pDto), Encoding.UTF8, "application/json");
            var res = await _client.PostAsync(request, content);
            var jsonString = await res.Content.ReadAsStringAsync();
            var dto = JsonConvert.DeserializeObject<DisplayPatientDto>(jsonString);
            Assert.NotNull(dto);
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
            var pDto2 = new CreatePatientDto(new CreateUserDto("adresa@gmail.com", "Test", "Test", "Test1234"), 15);

            var content2 = new StringContent(JsonConvert.SerializeObject(pDto2), Encoding.UTF8, "application/json");
            var res2 = await _client.PostAsync(request, content2);
            var pDto = new CreatePatientDto(new CreateUserDto("adresa@gmail.com", "Test", "Test", "Test1234"), 15);

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
            var pDto = new CreatePatientDto(new CreateUserDto("troller@gmail.com", "Test", "Test", "Test1234"), 150);


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
            var pDto = new CreatePatientDto(new CreateUserDto("adresa", "Test", "Test", "Test1234"), 15);


            var content = new StringContent(JsonConvert.SerializeObject(pDto), Encoding.UTF8, "application/json");
            var res = await _client.PostAsync(request, content);
            var actualJsonString = await res.Content.ReadAsStringAsync();

            // Then
            Assert.Equal(HttpStatusCode.BadRequest, res.StatusCode);
            Assert.Equal(PatientController.InvalidEmailError, actualJsonString);
        }

    }
}
