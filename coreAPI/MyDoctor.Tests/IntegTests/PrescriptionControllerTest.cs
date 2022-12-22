using MyDoctor.API.Controllers;
using MyDoctor.API.DTOs;
using MyDoctor.Application.Commands.MedicalRoomCommands;
using MyDoctor.Tests.Helpers;
using Newtonsoft.Json;
using System.Net;
using System.Text;

namespace MyDoctor.Tests.IntegTests
{
    public class PrescriptionControllerTest : BaseControllerTest<PrescriptionsController>
    {
        private Guid appointmentId;
        private Guid drug1Id;
        private Guid drug2Id;
        private Guid doctorId;
        private Guid patientId;

        public PrescriptionControllerTest(CustomWebApplicationFactory<Program> factory) : base(factory)
        {
        }

        private async Task Init()
        {
            string request2 = "https://localhost:7244/api/v1/Patients";
            var pDto = new CreatePatientDto(new CreateUserDto(RandomGenerators.CreateRandomEmail(), "Test1234", "Test", "Test"), 15);

            var content2 = new StringContent(JsonConvert.SerializeObject(pDto), Encoding.UTF8, "application/json");
            var resultPatient = await HttpClient.PostAsync(request2, content2);
            var jsonStringPatient = await resultPatient.Content.ReadAsStringAsync();
            var contentPatient = JsonConvert.DeserializeObject<DisplayPatientDto>(jsonStringPatient);
            Assert.NotNull(contentPatient);
            patientId = contentPatient.Id;


            string request3 = "https://localhost:7244/api/v1/MedicalRooms";
            CreateMedicalRoomCommand mdDto = new("Strada Cabina 20");

            var content3 = new StringContent(JsonConvert.SerializeObject(mdDto), Encoding.UTF8, "application/json");
            var res3 = await HttpClient.PostAsync(request3, content3);
            Assert.Equal(HttpStatusCode.OK, res3.StatusCode);

            string reqSpeciality = "https://localhost:7244/api/v1/Specialities/create_speciality";
            CreateSpecialityDto sDto = new("Chirurg");
            var contSpeciality = new StringContent(JsonConvert.SerializeObject(sDto), Encoding.UTF8, "application/json");
            var resSpeciality = await HttpClient.PostAsync(reqSpeciality, contSpeciality);
            var jsonCreatedSpeciality = await resSpeciality.Content.ReadAsStringAsync();
            var dtoSpeciality = JsonConvert.DeserializeObject<DisplaySpecialityDto>(jsonCreatedSpeciality);
            Assert.NotNull(dtoSpeciality);
            Assert.Equal(HttpStatusCode.OK, resSpeciality.StatusCode);

            string request4 = "https://localhost:7244/api/v1/Doctors/speciality";
            var pDto2 = new CreateDoctorDto(new CreateUserDto(RandomGenerators.CreateRandomEmail(), "Test1234", "Ion", "Cutelaba"), dtoSpeciality.Id);

            var content4 = new StringContent(JsonConvert.SerializeObject(pDto2), Encoding.UTF8, "application/json");
            var resultDoctor = await HttpClient.PostAsync(request4, content4);
            var jsonStringDoctor = await resultDoctor.Content.ReadAsStringAsync();
            var contentDoctor = JsonConvert.DeserializeObject<DisplayDoctorDto>(jsonStringDoctor);
            Assert.NotNull(contentDoctor);
            doctorId = contentDoctor.Id;

            string requestDrugStock = "https://localhost:7244/api/v1/DrugStocks";
            string requestDrug = "https://localhost:7244/api/v1/Drugs/{0}";
            var resultDrugStock = await HttpClient.GetAsync(requestDrugStock);

            var jsonString = await resultDrugStock.Content.ReadAsStringAsync();
            var contentDrugStocks = JsonConvert.DeserializeObject<List<DisplayDrugStockDto>>(jsonString);
            Assert.NotNull(contentDrugStocks);
            DisplayDrugStockDto drugStock = contentDrugStocks.First();

            CreateDrugDto drugDto1 = new("Peniciline is bad", "Not Peniciline", 35.64, 5);

            CreateDrugDto drugDto2 = new("Peniciline is good", "Peniciline", 25.48, 2);

            List<CreateDrugDto> dtos = new();
            dtos.Add(drugDto1);
            dtos.Add(drugDto2);
            var contentDrug = new StringContent(JsonConvert.SerializeObject(dtos), Encoding.UTF8, "application/json");

            var result = await HttpClient.PostAsync(string.Format(requestDrug, drugStock.Id.ToString()), contentDrug);
            var jsonString2 = await result.Content.ReadAsStringAsync();
            var drugsDtos = JsonConvert.DeserializeObject<List<DisplayDrugDto>>(jsonString2);
            Assert.NotNull(drugsDtos);
            drug1Id = drugsDtos[0].Id;
            drug2Id = drugsDtos[1].Id;

            Assert.Equal(HttpStatusCode.OK, result.StatusCode);

            string requestAppointment = $"https://localhost:7244/api/v1/Appointments/{contentPatient.Id}_{contentDoctor.Id}/create_appointment";
            var aDto = new CreateAppointmentDto(50, DateTime.Today.AddDays(1), DateTime.Now.AddHours(1), DateTime.Now.AddHours(5));
            var content = new StringContent(JsonConvert.SerializeObject(aDto), Encoding.UTF8, "application/json");
            var res = await HttpClient.PostAsync(string.Format(requestAppointment, patientId.ToString(), doctorId.ToString()), content);
            var jsonStringAppointment = await res.Content.ReadAsStringAsync();
            Assert.Equal(HttpStatusCode.OK, res.StatusCode);
            var appointmentDto = JsonConvert.DeserializeObject<DisplayAppointmentDto>(jsonStringAppointment);
            Assert.NotNull(appointmentDto);
            appointmentId = appointmentDto.Id;
        }

        [Fact]
        public async Task TestCreatePrescription_Valid()
        {

            // Given
            await Init();

            // When
            string request = "https://localhost:7244/api/v1/Prescriptions/{0}";
            var procedure1dto = new CreateProcedureDto("Taierea piciorului", "Taierea piciorului drept cu cutitul", 64);
            var procedure2dto = new CreateProcedureDto("Taierea piciorului", "Taierea piciorului stang cu cutitul", 64);
            var pDto = new CreatePrescriptionDto("Amputare lunga si scurta", "Amputare", new List<GetDrugDto>() { new GetDrugDto(drug1Id, 2), new GetDrugDto(drug2Id, 1) },
                new List<CreateProcedureDto>() { procedure1dto, procedure2dto });

            var content = new StringContent(JsonConvert.SerializeObject(pDto), Encoding.UTF8, "application/json");
            var res = await HttpClient.PostAsync(string.Format(request, appointmentId.ToString()), content);
            var jsonString = await res.Content.ReadAsStringAsync();
            Assert.Equal(HttpStatusCode.OK, res.StatusCode);
            var dto = JsonConvert.DeserializeObject<DisplayPrescriptionDto>(jsonString);
            Assert.NotNull(dto);
            Assert.True(dto.Name == pDto.Name);
            Assert.True(dto.Description == pDto.Description);
            Assert.True(dto.AppointmentId == appointmentId);
        }

        [Fact]
        public async Task TestCreatePrescription_WrongAppointmentId()
        {
            await Init();
            string request = "https://localhost:7244/api/v1/Prescriptions/{0}";
            var procedure1dto = new CreateProcedureDto("Taierea piciorului", "Taierea piciorului drept cu cutitul", 64);
            var procedure2dto = new CreateProcedureDto("Taierea piciorului", "Taierea piciorului stang cu cutitul", 64);
            var pDto = new CreatePrescriptionDto("Amputare lunga si sccurta", "Amputare", new List<GetDrugDto>() { new GetDrugDto(drug1Id, 2), new GetDrugDto(drug2Id, 1) },
                new List<CreateProcedureDto>() { procedure1dto, procedure2dto });

            var content = new StringContent(JsonConvert.SerializeObject(pDto), Encoding.UTF8, "application/json");
            var res = await HttpClient.PostAsync(string.Format(request, Guid.NewGuid().ToString()), content);
            var jsonString = await res.Content.ReadAsStringAsync();
            Assert.Equal(HttpStatusCode.NotFound, res.StatusCode);
            Assert.Equal(PrescriptionsController.AppointmentNotFoundError, jsonString);
        }

        [Fact]
        public async Task TestCreatePrescription_DoctorNotFound()
        {
            await Init();
            string requestDoctor = "https://localhost:7244/api/v1/Doctors/{0}";
            var resultDoctorDelete = await HttpClient.DeleteAsync(string.Format(requestDoctor, doctorId.ToString()));
            Assert.Equal(HttpStatusCode.OK, resultDoctorDelete.StatusCode);
            string request = "https://localhost:7244/api/v1/Prescriptions/{0}";
            var procedure1dto = new CreateProcedureDto("Taierea piciorului", "Taierea piciorului drept cu cutitul", 64);
            var procedure2dto = new CreateProcedureDto("Taierea piciorului", "Taierea piciorului stang cu cutitul", 64);
            var pDto = new CreatePrescriptionDto("Amputare lunga si scurta", "Amputare", new List<GetDrugDto>() { new GetDrugDto(drug1Id, 2), new GetDrugDto(drug2Id, 1) },
                new List<CreateProcedureDto>() { procedure1dto, procedure2dto });

            var content = new StringContent(JsonConvert.SerializeObject(pDto), Encoding.UTF8, "application/json");
            var res = await HttpClient.PostAsync(string.Format(request, appointmentId.ToString()), content);
            var jsonString = await res.Content.ReadAsStringAsync();
            Assert.Equal(HttpStatusCode.NotFound, res.StatusCode);
            Assert.Equal(PrescriptionsController.AppointmentNotFoundError, jsonString);
        }
        [Fact]
        public async Task TestCreatePrescription_DrugNotFound()
        {
            await Init();
            string request = "https://localhost:7244/api/v1/Prescriptions/{0}";
            var procedure1dto = new CreateProcedureDto("Taierea piciorului", "Taierea piciorului drept cu cutitul", 64);
            var procedure2dto = new CreateProcedureDto("Taierea piciorului", "Taierea piciorului stang cu cutitul", 64);
            var pDto = new CreatePrescriptionDto("Amputare lunga si scurta", "Amputare", new List<GetDrugDto>() { new GetDrugDto(Guid.NewGuid(), 2), new GetDrugDto(drug2Id, 1) },
                new List<CreateProcedureDto>() { procedure1dto, procedure2dto });

            var content = new StringContent(JsonConvert.SerializeObject(pDto), Encoding.UTF8, "application/json");
            var res = await HttpClient.PostAsync(string.Format(request, appointmentId.ToString()), content);
            var jsonString = await res.Content.ReadAsStringAsync();
            Assert.Equal(HttpStatusCode.NotFound, res.StatusCode);
            Assert.Equal(PrescriptionsController.DrugCreateError, jsonString);
        }
    }
}
