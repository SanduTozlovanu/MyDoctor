using MyDoctor.API.Controllers;
using MyDoctor.API.DTOs;
using MyDoctor.Application.Commands.MedicalRoomCommands;
using MyDoctor.Application.Responses;
using MyDoctor.Tests.Helpers;
using Newtonsoft.Json;
using System.Net;
using System.Text;

namespace MyDoctor.Tests.IntegTests
{
    public class DrugControllerTest : BaseControllerTest<DrugsController>
    {
        public DrugControllerTest(CustomWebApplicationFactory<Program> factory) : base(factory)
        {
        }

        [Fact]
        public async Task TestGetDrugs()
        {

            // When
            string request = "https://localhost:7244/api/v1/MedicalRooms";
            string request3 = "https://localhost:7244/api/v1/Drugs/{0}";
            string reqSpeciality = "https://localhost:7244/api/v1/Specialities/create_speciality";
            string createDoctorRequest = "https://localhost:7244/api/v1/Doctors/speciality";
            CreateMedicalRoomCommand mdDto1 = new("Strada Farmaciei 8");

            var content1 = new StringContent(JsonConvert.SerializeObject(mdDto1), Encoding.UTF8, "application/json");
            var res1 = await HttpClient.PostAsync(request, content1);

            Assert.Equal(HttpStatusCode.OK, res1.StatusCode);

            var jsonString1 = await res1.Content.ReadAsStringAsync();
            var cont1 = JsonConvert.DeserializeObject<MedicalRoomResponse>(jsonString1);
            Assert.NotNull(cont1);
            MedicalRoomResponse medRoom1 = new(cont1.Id, mdDto1.Adress);

            CreateSpecialityDto sDto = new("Chirurg");
            var contSpeciality = new StringContent(JsonConvert.SerializeObject(sDto), Encoding.UTF8, "application/json");
            var resSpeciality = await HttpClient.PostAsync(reqSpeciality, contSpeciality);
            var jsonCreatedSpeciality = await resSpeciality.Content.ReadAsStringAsync();
            var dtoSpeciality = JsonConvert.DeserializeObject<DisplaySpecialityDto>(jsonCreatedSpeciality);
            Assert.NotNull(dtoSpeciality);
            Assert.Equal(HttpStatusCode.OK, resSpeciality.StatusCode);
            var dDto = new CreateDoctorDto(new CreateUserDto(RandomGenerators.CreateRandomEmail(), "Testurilef51sf5", "Cutelaba", "Test1234"), dtoSpeciality.Id);

            var createDoctorContent = new StringContent(JsonConvert.SerializeObject(dDto), Encoding.UTF8, "application/json");
            var res = await HttpClient.PostAsync(createDoctorRequest, createDoctorContent);
            var jsonString = await res.Content.ReadAsStringAsync();
            var doctorDisplay = JsonConvert.DeserializeObject<DisplayDoctorDto>(jsonString);
            Assert.NotNull(doctorDisplay);
            Assert.True(doctorDisplay.FirstName == dDto.UserDetails.FirstName);
            Assert.True(doctorDisplay.Email == dDto.UserDetails.Email);
            Assert.True(doctorDisplay.LastName == dDto.UserDetails.LastName);

            CreateDrugDto drugDto1 = new("Peniciline is bad", "Not Peniciline", 35.64, 5);

            CreateDrugDto drugDto2 = new("Peniciline is good", "Peniciline", 25.48, 2);

            List<CreateDrugDto> dtos = new()
            {
                drugDto1,
                drugDto2
            };
            var content = new StringContent(JsonConvert.SerializeObject(dtos), Encoding.UTF8, "application/json");

            var result = await HttpClient.PostAsync(string.Format(request3, doctorDisplay.Id.ToString()), content);
            var jsonString2 = await result.Content.ReadAsStringAsync();
            var displayDtos = JsonConvert.DeserializeObject<List<DisplayDrugDto>>(jsonString2);
            Assert.NotNull(displayDtos);
            Assert.Equal(2, displayDtos.Count);
            displayDtos.ForEach(dto => Assert.True(dto.Equals(new DisplayDrugDto
                (dto.Id, dto.DrugStockId, drugDto1.Name, drugDto1.Description, drugDto1.Price, drugDto1.Quantity))
                 || dto.Equals(new DisplayDrugDto
                (dto.Id, dto.DrugStockId, drugDto2.Name, drugDto2.Description, drugDto2.Price, drugDto2.Quantity))));

        }
        [Fact]
        public async Task TestGetDrugs_WrongDrugStockId()
        {
            string request3 = "https://localhost:7244/api/v1/Drugs/{0}";

            CreateDrugDto drugDto1 = new("Peniciline is bad", "Not Peniciline", 35.64, 5);

            CreateDrugDto drugDto2 = new("Peniciline is good", "Peniciline", 25.48, 2);

            List<CreateDrugDto> dtos = new();
            dtos.Add(drugDto1);
            dtos.Add(drugDto2);
            var content = new StringContent(JsonConvert.SerializeObject(dtos), Encoding.UTF8, "application/json");

            var result = await HttpClient.PostAsync(string.Format(request3, "48db124a-e731-4664-9add-44172a403b90"), content);
            var jsonString1 = await result.Content.ReadAsStringAsync();
            Assert.Equal(HttpStatusCode.NotFound, result.StatusCode);
            Assert.Equal(DrugsController.DoctorNotFoundError, jsonString1);
        }
    }
}
