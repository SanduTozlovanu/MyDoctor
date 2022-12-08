﻿using Microsoft.AspNetCore.Mvc.Testing;
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
            var pDto = new CreateDoctorDto(new CreateUserDto("doctor@gmail.com", "Ion", "Cutelaba", "Test1234"), "Chirurg");

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
            CreateMedicalRoomDto mdDto = new("Strada Cabina 20");

            var content2 = new StringContent(JsonConvert.SerializeObject(mdDto), Encoding.UTF8, "application/json");
            var res2 = await _client.PostAsync(request2, content2);
            Assert.Equal(HttpStatusCode.OK, res2.StatusCode);

            string request = "https://localhost:7244/api/Doctor";
            var pDto = new CreateDoctorDto(new CreateUserDto("doctor@gmail.com", "Ion", "Cutelaba", "Test1234"),"Chirurg");

            var content = new StringContent(JsonConvert.SerializeObject(pDto), Encoding.UTF8, "application/json");
            var res = await _client.PostAsync(request, content);
            var jsonString = await res.Content.ReadAsStringAsync();
            var dto = JsonConvert.DeserializeObject<DisplayDoctorDto>(jsonString);
            Assert.NotNull(dto);
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
            var pDto = new CreateDoctorDto(new CreateUserDto("doctor@gmail.com", "Test", "Test", "Test1234"),"Oculist");

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
            var pDto = new CreateDoctorDto(new CreateUserDto("adresa", "Test", "Test", "Test1234"), "Anarhist");

            var content = new StringContent(JsonConvert.SerializeObject(pDto), Encoding.UTF8, "application/json");
            var res = await _client.PostAsync(request, content);
            var actualJsonString = await res.Content.ReadAsStringAsync();

            // Then
            Assert.Equal(HttpStatusCode.BadRequest, res.StatusCode);
            Assert.Equal(DoctorController.InvalidEmailError, actualJsonString);
        }

    }
}