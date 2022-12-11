﻿using Microsoft.AspNetCore.Mvc.Testing;
using MyDoctor.API.Controllers;
using MyDoctor.API.DTOs;
using MyDoctor.Tests.Helpers;
using MyDoctor.Tests.Orderers;
using MyDoctorApp.Domain.Models;
using Newtonsoft.Json;
using System.Net;
using System.Text;

namespace MyDoctor.Tests.IntegTests
{
    [TestCaseOrderer("MyDoctor.Tests.Orderers.PriorityOrderer", "MyDoctor.Tests")]
    public class PrescriptionControllerTest : IClassFixture<DatabaseFixture>
    {
        private readonly HttpClient _client;
        private DatabaseFixture databaseFixture;
        private Guid appointmentId;
        private Guid drug1Id;
        private Guid drug2Id;
        private Guid doctorId;
        private Guid patientId;

        // Ctor is called for every test method
        public PrescriptionControllerTest(DatabaseFixture databaseFixture)
        {
            var app = new WebApplicationFactory<PatientController>()
                .WithWebHostBuilder(builder => { });
            _client = app.CreateClient();
            this.databaseFixture = databaseFixture;
        }


        private async Task Init()
        {
            string request2 = "https://localhost:7244/api/Patient";
            var pDto = new CreatePatientDto(new CreateUserDto(User.CreateRandomEmail(), "Test", "Test", "Test1234"), 15);

            var content2 = new StringContent(JsonConvert.SerializeObject(pDto), Encoding.UTF8, "application/json");
            var resultPatient = await _client.PostAsync(request2, content2);
            var jsonStringPatient = await resultPatient.Content.ReadAsStringAsync();
            var contentPatient = JsonConvert.DeserializeObject<DisplayPatientDto>(jsonStringPatient);
            Assert.NotNull(contentPatient);
            patientId = contentPatient.Id;


            string request3 = "https://localhost:7244/api/MedicalRoom";
            CreateMedicalRoomDto mdDto = new("Strada Cabina 20");

            var content3 = new StringContent(JsonConvert.SerializeObject(mdDto), Encoding.UTF8, "application/json");
            var res3 = await _client.PostAsync(request3, content3);
            Assert.Equal(HttpStatusCode.OK, res3.StatusCode);

            string request4 = "https://localhost:7244/api/Doctor";
            var pDto2 = new CreateDoctorDto(new CreateUserDto(User.CreateRandomEmail(), "Ion", "Cutelaba", "Test1234"), "Chirurg");

            var content4 = new StringContent(JsonConvert.SerializeObject(pDto2), Encoding.UTF8, "application/json");
            var resultDoctor = await _client.PostAsync(request4, content4);
            var jsonStringDoctor = await resultDoctor.Content.ReadAsStringAsync();
            var contentDoctor = JsonConvert.DeserializeObject<DisplayDoctorDto>(jsonStringDoctor);
            Assert.NotNull(contentDoctor);
            doctorId = contentDoctor.Id;

            string requestDrugStock = "https://localhost:7244/api/DrugStock";
            string requestDrug = "https://localhost:7244/api/Drug/{0}";
            var resultDrugStock = await _client.GetAsync(requestDrugStock);

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

            var result = await _client.PostAsync(string.Format(requestDrug, drugStock.Id.ToString()), contentDrug);
            var jsonString2 = await result.Content.ReadAsStringAsync();
            var drugsDtos = JsonConvert.DeserializeObject<List<DisplayDrugDto>>(jsonString2);
            Assert.NotNull(drugsDtos);
            drug1Id = drugsDtos[0].Id;
            drug2Id = drugsDtos[1].Id;

            Assert.Equal(HttpStatusCode.OK, result.StatusCode);

            string requestAppointment = "https://localhost:7244/api/Appointment/{0}_{1}/create_appointment";
            var aDto = new CreateAppointmentDto(50, DateTime.Today, DateTime.Now, DateTime.Now.AddHours(5));
            var content = new StringContent(JsonConvert.SerializeObject(aDto), Encoding.UTF8, "application/json");
            var res = await _client.PostAsync(string.Format(requestAppointment, patientId.ToString(), doctorId.ToString()), content);
            Assert.Equal(HttpStatusCode.OK, res.StatusCode);
            var jsonStringAppointment = await res.Content.ReadAsStringAsync();
            var appointmentDto = JsonConvert.DeserializeObject<DisplayAppointmentDto>(jsonStringAppointment);
            Assert.NotNull(appointmentDto);
            appointmentId = appointmentDto.Id;
        }

        [Fact, TestPriority(0)]
        public async Task TestCreatePrescription_Valid()
        {

            // Given
            await Init();

            // When
            string request = "https://localhost:7244/api/Prescription/{0}";
            var procedure1dto = new CreateProcedureDto("Taierea piciorului","Taierea piciorului drept cu cutitul", 64);
            var procedure2dto = new CreateProcedureDto("Taierea piciorului", "Taierea piciorului stang cu cutitul", 64);
            var pDto = new CreatePrescriptionDto("Amputare", "Amputare", new List<GetDrugDto>() { new GetDrugDto(drug1Id, 2), new GetDrugDto(drug2Id, 1) },
                new List<CreateProcedureDto>() { procedure1dto , procedure2dto });

            var content = new StringContent(JsonConvert.SerializeObject(pDto), Encoding.UTF8, "application/json");
            var res = await _client.PostAsync(string.Format(request, appointmentId.ToString()), content);
            Assert.Equal(HttpStatusCode.OK, res.StatusCode);
            var jsonString = await res.Content.ReadAsStringAsync();
            var dto = JsonConvert.DeserializeObject<DisplayPrescriptionDto>(jsonString);
            Assert.NotNull(dto);
            Assert.True(dto.Name == pDto.Name);
            Assert.True(dto.Description == pDto.Description);
            Assert.True(dto.AppointmentId == appointmentId);
        }

        [Fact, TestPriority(1)]
        public async Task TestCreatePrescription_WrongAppointmentId()
        {
            await Init();
            string request = "https://localhost:7244/api/Prescription/{0}";
            var procedure1dto = new CreateProcedureDto("Taierea piciorului", "Taierea piciorului drept cu cutitul", 64);
            var procedure2dto = new CreateProcedureDto("Taierea piciorului", "Taierea piciorului stang cu cutitul", 64);
            var pDto = new CreatePrescriptionDto("Amputare", "Amputare", new List<GetDrugDto>() { new GetDrugDto(drug1Id, 2), new GetDrugDto(drug2Id, 1) },
                new List<CreateProcedureDto>() { procedure1dto, procedure2dto });

            var content = new StringContent(JsonConvert.SerializeObject(pDto), Encoding.UTF8, "application/json");
            var res = await _client.PostAsync(string.Format(request, Guid.NewGuid().ToString()), content);
            var jsonString = await res.Content.ReadAsStringAsync();
            Assert.Equal(HttpStatusCode.NotFound, res.StatusCode);
            Assert.Equal(PrescriptionController.AppointmentNotFoundError, jsonString);
        }

        [Fact, TestPriority(2)]
        public async Task TestCreatePrescription_DoctorNotFound()
        {
            await Init();
            string requestDoctor = "https://localhost:7244/api/Doctor/{0}";
            var resultDoctorDelete = await _client.DeleteAsync(string.Format(requestDoctor, doctorId.ToString()));
            Assert.Equal(HttpStatusCode.OK, resultDoctorDelete.StatusCode);
            string request = "https://localhost:7244/api/Prescription/{0}";
            var procedure1dto = new CreateProcedureDto("Taierea piciorului", "Taierea piciorului drept cu cutitul", 64);
            var procedure2dto = new CreateProcedureDto("Taierea piciorului", "Taierea piciorului stang cu cutitul", 64);
            var pDto = new CreatePrescriptionDto("Amputare", "Amputare", new List<GetDrugDto>() { new GetDrugDto(drug1Id, 2), new GetDrugDto(drug2Id, 1) },
                new List<CreateProcedureDto>() { procedure1dto, procedure2dto });

            var content = new StringContent(JsonConvert.SerializeObject(pDto), Encoding.UTF8, "application/json");
            var res = await _client.PostAsync(string.Format(request, appointmentId.ToString()), content);
            var jsonString = await res.Content.ReadAsStringAsync();
            Assert.Equal(HttpStatusCode.NotFound, res.StatusCode);
            Assert.Equal(PrescriptionController.AppointmentNotFoundError, jsonString);
        }
        [Fact, TestPriority(3)]
        public async Task TestCreatePrescription_DrugNotFound()
        {
            await Init();
            string request = "https://localhost:7244/api/Prescription/{0}";
            var procedure1dto = new CreateProcedureDto("Taierea piciorului", "Taierea piciorului drept cu cutitul", 64);
            var procedure2dto = new CreateProcedureDto("Taierea piciorului", "Taierea piciorului stang cu cutitul", 64);
            var pDto = new CreatePrescriptionDto("Amputare", "Amputare", new List<GetDrugDto>() { new GetDrugDto(Guid.NewGuid(), 2), new GetDrugDto(drug2Id, 1) },
                new List<CreateProcedureDto>() { procedure1dto, procedure2dto });

            var content = new StringContent(JsonConvert.SerializeObject(pDto), Encoding.UTF8, "application/json");
            var res = await _client.PostAsync(string.Format(request, appointmentId.ToString()), content);
            var jsonString = await res.Content.ReadAsStringAsync();
            Assert.Equal(HttpStatusCode.NotFound, res.StatusCode);
            Assert.Equal(PrescriptionController.DrugCreateError, jsonString);
        }
    }
}