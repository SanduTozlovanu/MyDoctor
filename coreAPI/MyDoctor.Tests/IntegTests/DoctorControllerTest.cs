using MyDocAppointment.API.Validations;
using MyDoctor.API.Controllers;
using MyDoctor.API.DTOs;
using MyDoctor.Application.Commands.MedicalRoomCommands;
using MyDoctor.Tests.Helpers;
using Newtonsoft.Json;
using System.Net;
using System.Text;

namespace MyDoctor.Tests.IntegTests
{
    public class DoctorControllerTest : BaseControllerTest<DoctorsController>
    {
        public DoctorControllerTest(CustomWebApplicationFactory<Program> factory) : base(factory)
        {
        }

        private async Task Init()
        {
            string request2 = "https://localhost:7244/api/v1/MedicalRooms";
            CreateMedicalRoomCommand mdDto = new("Strada Cabina 20");

            var content2 = new StringContent(JsonConvert.SerializeObject(mdDto), Encoding.UTF8, "application/json");
            var res2 = await HttpClient.PostAsync(request2, content2);
            Assert.Equal(HttpStatusCode.OK, res2.StatusCode);


        }
        [Fact]
        public async Task TestCreateDoctor_NoMedicalRoom()
        {
            string reqSpeciality = "https://localhost:7244/api/v1/Specialities/create_speciality";
            CreateSpecialityDto sDto = new("Chirurg");
            var contSpeciality = new StringContent(JsonConvert.SerializeObject(sDto), Encoding.UTF8, "application/json");
            var resSpeciality = await HttpClient.PostAsync(reqSpeciality, contSpeciality);
            var jsonCreatedSpeciality = await resSpeciality.Content.ReadAsStringAsync();
            var dtoSpeciality = JsonConvert.DeserializeObject<DisplaySpecialityDto>(jsonCreatedSpeciality);
            Assert.NotNull(dtoSpeciality);
            Assert.Equal(HttpStatusCode.OK, resSpeciality.StatusCode);

            string request = "https://localhost:7244/api/v1/Doctors/speciality";
            var pDto = new CreateDoctorDto(new CreateUserDto(RandomGenerators.CreateRandomEmail(), "Testurilef51sf5", "Cutelaba", "Test1234"), dtoSpeciality.Id);

            var content = new StringContent(JsonConvert.SerializeObject(pDto), Encoding.UTF8, "application/json");
            var res = await HttpClient.PostAsync(request, content);
            var jsonString = await res.Content.ReadAsStringAsync();
            Assert.Equal(HttpStatusCode.NotFound, res.StatusCode);
            Assert.Equal(DoctorsController.FreeMedicalRoomNotFoundError, jsonString);

        }

        [Fact]
        public async Task TestCreateDoctor_Valid()
        {
            await Init();

            string reqSpeciality = "https://localhost:7244/api/v1/Specialities/create_speciality";
            CreateSpecialityDto sDto = new("Chirurg");
            var contSpeciality = new StringContent(JsonConvert.SerializeObject(sDto), Encoding.UTF8, "application/json");
            var resSpeciality = await HttpClient.PostAsync(reqSpeciality, contSpeciality);
            var jsonCreatedSpeciality = await resSpeciality.Content.ReadAsStringAsync();
            var dtoSpeciality = JsonConvert.DeserializeObject<DisplaySpecialityDto>(jsonCreatedSpeciality);
            Assert.NotNull(dtoSpeciality);
            Assert.Equal(HttpStatusCode.OK, resSpeciality.StatusCode);

            string request = "https://localhost:7244/api/v1/Doctors/speciality";
            var pDto = new CreateDoctorDto(new CreateUserDto(RandomGenerators.CreateRandomEmail(), "Testurilef51sf5", "Cutelaba", "Test1234"), dtoSpeciality.Id);

            var content = new StringContent(JsonConvert.SerializeObject(pDto), Encoding.UTF8, "application/json");
            var res = await HttpClient.PostAsync(request, content);
            var jsonString = await res.Content.ReadAsStringAsync();
            var dto = JsonConvert.DeserializeObject<DisplayDoctorDto>(jsonString);
            Assert.NotNull(dto);
            Assert.True(dto.FirstName == pDto.UserDetails.FirstName);
            Assert.True(dto.Email == pDto.UserDetails.Email);
            Assert.True(dto.LastName == pDto.UserDetails.LastName);
        }

        [Fact]
        public async Task TestCreateDoctor_AlreadyUsedEmail()
        {

            // Given
            await Init();
            string reqSpeciality = "https://localhost:7244/api/v1/Specialities/create_speciality";
            CreateSpecialityDto sDto = new("Chirurg");
            var contSpeciality = new StringContent(JsonConvert.SerializeObject(sDto), Encoding.UTF8, "application/json");
            var resSpeciality = await HttpClient.PostAsync(reqSpeciality, contSpeciality);
            var jsonCreatedSpeciality = await resSpeciality.Content.ReadAsStringAsync();
            var dtoSpeciality = JsonConvert.DeserializeObject<DisplaySpecialityDto>(jsonCreatedSpeciality);
            Assert.NotNull(dtoSpeciality);
            Assert.Equal(HttpStatusCode.OK, resSpeciality.StatusCode);
            // When
            string request = "https://localhost:7244/api/v1/Doctors/speciality";
            var pDto = new CreateDoctorDto(new CreateUserDto("doctor@gmail.com", "Testurilef51sf5", "Test", "Test1234"), dtoSpeciality.Id);

            var content = new StringContent(JsonConvert.SerializeObject(pDto), Encoding.UTF8, "application/json");
            await HttpClient.PostAsync(request, content);
            var res2 = await HttpClient.PostAsync(request, content);
            var actualJsonString = await res2.Content.ReadAsStringAsync();

            // Then
            Assert.Equal(HttpStatusCode.BadRequest, res2.StatusCode);
            Assert.Equal(DoctorsController.UsedEmailError, actualJsonString);
        }

        [Fact]
        public async Task TestCreateDoctor_InvalidEmail()
        {

            // Given
            await Init();

            string reqSpeciality = "https://localhost:7244/api/v1/Specialities/create_speciality";
            CreateSpecialityDto sDto = new("Chirurg");
            var contSpeciality = new StringContent(JsonConvert.SerializeObject(sDto), Encoding.UTF8, "application/json");
            var resSpeciality = await HttpClient.PostAsync(reqSpeciality, contSpeciality);
            var jsonCreatedSpeciality = await resSpeciality.Content.ReadAsStringAsync();
            var dtoSpeciality = JsonConvert.DeserializeObject<DisplaySpecialityDto>(jsonCreatedSpeciality);
            Assert.NotNull(dtoSpeciality);
            Assert.Equal(HttpStatusCode.OK, resSpeciality.StatusCode);

            // When
            string request = "https://localhost:7244/api/v1/Doctors/speciality";
            var pDto = new CreateDoctorDto(new CreateUserDto("adresa", "Testurilef51sf5", "Test", "Test1234"), dtoSpeciality.Id);

            var content = new StringContent(JsonConvert.SerializeObject(pDto), Encoding.UTF8, "application/json");
            var res = await HttpClient.PostAsync(request, content);
            var actualJsonString = await res.Content.ReadAsStringAsync();

            // Then
            Assert.Equal(HttpStatusCode.BadRequest, res.StatusCode);
            Assert.Contains(UserValidator.INVALID_MAIL_ERROR, actualJsonString);
        }

    }
}