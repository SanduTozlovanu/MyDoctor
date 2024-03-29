﻿using Microsoft.Extensions.DependencyInjection;
using MyDoctor.API.Controllers;
using MyDoctor.Application.Commands.MedicalRoomCommands;
using MyDoctor.Application.Responses;
using MyDoctor.Tests.Helpers;
using MyDoctorApp.Domain.Models;
using MyDoctorApp.Infrastructure;
using Newtonsoft.Json;
using System.Net;
using System.Text;

namespace MyDoctor.Tests.IntegTests
{
    public class MedicalRoomControllerTest : BaseControllerTest<MedicalRoomsController>
    {
        private readonly string Address1 = "Ion Putulai 5";
        private readonly string Address2 = "Ana Anastasie 32154";

        public MedicalRoomControllerTest(CustomWebApplicationFactory<Program> factory) : base(factory)
        {
        }

        private async Task Init()
        {
            string request = "https://localhost:7244/api/v1/MedicalRooms";
            CreateMedicalRoomCommand mdDto = new(Address1);

            var content = new StringContent(JsonConvert.SerializeObject(mdDto), Encoding.UTF8, "application/json");
            var res = await HttpClient.PostAsync(request, content);
            Assert.Equal(HttpStatusCode.OK, res.StatusCode);
        }

        [Fact]
        public async Task TestCreateMedicalRoom()
        {
            string request = "https://localhost:7244/api/v1/MedicalRooms";
            CreateMedicalRoomCommand mdDto = new(Address1);
            CreateMedicalRoomCommand mdDto2 = new(Address2);

            var content = new StringContent(JsonConvert.SerializeObject(mdDto), Encoding.UTF8, "application/json");
            var content2 = new StringContent(JsonConvert.SerializeObject(mdDto2), Encoding.UTF8, "application/json");
            var res = await HttpClient.PostAsync(request, content);
            var res2 = await HttpClient.PostAsync(request, content2);
            Assert.Equal(HttpStatusCode.OK, res.StatusCode);
            Assert.Equal(HttpStatusCode.OK, res2.StatusCode);
            var jsonString = await res.Content.ReadAsStringAsync();
            var jsonString2 = await res2.Content.ReadAsStringAsync();
            var cont = JsonConvert.DeserializeObject<MedicalRoomResponse>(jsonString);
            var cont2 = JsonConvert.DeserializeObject<MedicalRoomResponse>(jsonString2);
            Assert.NotNull(cont);
            Assert.NotNull(cont2);
            MedicalRoomResponse expectedObject = new(cont.Id, mdDto.Adress);
            MedicalRoomResponse expectedObject2 = new(cont2.Id, mdDto2.Adress);
            Assert.True(expectedObject.Equals(cont));
            Assert.True(expectedObject2.Equals(cont2));
            using var scope = Factory.Services.CreateScope();
            var scopeProvider = scope.ServiceProvider;
            var databaseContext = scopeProvider.GetRequiredService<DatabaseContext>();

            MedicalRoom? medicalRoom1 = await databaseContext.MedicalRooms.FindAsync(cont.Id);
            MedicalRoom? medicalRoom2 = await databaseContext.MedicalRooms.FindAsync(cont2.Id);
            Assert.NotNull(medicalRoom1);
            Assert.NotNull(medicalRoom2);
            Assert.Equal(medicalRoom1.Adress, mdDto.Adress);
            Assert.Equal(medicalRoom1.Id, cont.Id);
            Assert.Equal(medicalRoom2.Adress, mdDto2.Adress);
            Assert.Equal(medicalRoom2.Id, cont2.Id);

        }

        [Fact]
        public async Task TestGetMedicalRooms()
        {
            await Init();
            string request = "https://localhost:7244/api/v1/MedicalRooms";
            CreateMedicalRoomCommand mdDto = new(Address1);
            CreateMedicalRoomCommand mdDto2 = new(Address2);

            var content = new StringContent(JsonConvert.SerializeObject(mdDto), Encoding.UTF8, "application/json");
            var content2 = new StringContent(JsonConvert.SerializeObject(mdDto2), Encoding.UTF8, "application/json");
            var res = await HttpClient.PostAsync(request, content);
            var jsonString3 = await res.Content.ReadAsStringAsync();
            var res2 = await HttpClient.PostAsync(request, content2);
            Assert.Equal(HttpStatusCode.OK, res.StatusCode);
            Assert.Equal(HttpStatusCode.OK, res2.StatusCode);
            request = "https://localhost:7244/api/v1/MedicalRooms";
            res = await HttpClient.GetAsync(request);

            var jsonString = await res.Content.ReadAsStringAsync();
            var cont = JsonConvert.DeserializeObject<List<MedicalRoomResponse>>(jsonString);
            Assert.NotNull(cont);
            Assert.True(cont.Count >= 2);
            bool foundObject = false;
            cont.ForEach(dto =>
            {
                if (dto.Adress == Address1)
                    foundObject = true;
            });
            Assert.True(foundObject);
        }
    }
}
