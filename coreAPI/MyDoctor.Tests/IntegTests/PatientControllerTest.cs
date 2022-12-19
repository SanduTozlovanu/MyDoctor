using MyDoctor.API.Controllers;
using MyDoctor.API.DTOs;
using MyDoctor.Tests.Helpers;
using Newtonsoft.Json;
using System.Net;
using System.Text;

namespace MyDoctor.Tests.IntegTests
{
    public class PatientControllerTest : BaseControllerTest<PatientController>
    {
        public PatientControllerTest(CustomWebApplicationFactory<Program> factory) : base(factory)
        {
        }

        [Fact]
        public async Task TestCreatePacient_Valid()
        {

            // Given

            // When
            string request = "https://localhost:7244/api/Patient";
            var pDto = new CreatePatientDto(new CreateUserDto(RandomGenerators.CreateRandomEmail(), "Test1234", "Test", "Test"), 15);

            var content = new StringContent(JsonConvert.SerializeObject(pDto), Encoding.UTF8, "application/json");
            var res = await HttpClient.PostAsync(request, content);
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

        [Fact]
        public async Task TestCreatePacient_AlreadyUsedEmail()
        {

            // Given

            // When
            string request = "https://localhost:7244/api/Patient";
            var pDto2 = new CreatePatientDto(new CreateUserDto("adresa@gmail.com", "Test1234", "Test", "Test"), 15);

            var content2 = new StringContent(JsonConvert.SerializeObject(pDto2), Encoding.UTF8, "application/json");
            var res2 = await HttpClient.PostAsync(request, content2);
            var pDto = new CreatePatientDto(new CreateUserDto("adresa@gmail.com", "Test1234", "Test", "Test"), 15);

            var content = new StringContent(JsonConvert.SerializeObject(pDto), Encoding.UTF8, "application/json");
            var res = await HttpClient.PostAsync(request, content);
            var actualJsonString = await res.Content.ReadAsStringAsync();

            // Then
            Assert.Equal(HttpStatusCode.BadRequest, res.StatusCode);
            Assert.Equal(PatientController.UsedEmailError, actualJsonString);
        }

        [Fact]
        public async Task TestCreatePacient_BigAge()
        {

            // Given

            // When
            string request = "https://localhost:7244/api/Patient";
            var pDto = new CreatePatientDto(new CreateUserDto("troller@gmail.com", "Test1234", "Test", "Test"), 100);


            var content = new StringContent(JsonConvert.SerializeObject(pDto), Encoding.UTF8, "application/json");
            var res = await HttpClient.PostAsync(request, content);
            var actualJsonString = await res.Content.ReadAsStringAsync();

            // Then
            Assert.Equal(HttpStatusCode.OK, res.StatusCode);
        }

        [Fact]
        public async Task TestCreatePacient_ValidEmail()
        {

            // Given

            // When
            string request = "https://localhost:7244/api/Patient";
            var pDto = new CreatePatientDto(new CreateUserDto(RandomGenerators.CreateRandomEmail(), "Test1234", "Test", "Test"), 15);


            var content = new StringContent(JsonConvert.SerializeObject(pDto), Encoding.UTF8, "application/json");
            var res = await HttpClient.PostAsync(request, content);
            var actualJsonString = await res.Content.ReadAsStringAsync();

            // Then
            Assert.Equal(HttpStatusCode.OK, res.StatusCode);
        }

    }
}
