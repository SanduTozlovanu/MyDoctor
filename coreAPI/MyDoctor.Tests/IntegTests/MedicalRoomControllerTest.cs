using Microsoft.AspNetCore.Mvc.Testing;
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
    public class MedicalRoomControllerTest : BaseControllerTest<MedicalRoomController>
    {
        private readonly string Address1 = "Ion Putulai 5";
        private readonly string Address2 = "Ana Anastasie 32154";

        private async void Init()
        {
            string request = "https://localhost:7244/api/MedicalRoom";
            CreateMedicalRoomDto mdDto = new(Address1);

            var content = new StringContent(JsonConvert.SerializeObject(mdDto), Encoding.UTF8, "application/json");
            var res = await _client.PostAsync(request, content);
            Assert.Equal(HttpStatusCode.OK, res.StatusCode);
        }

        [Fact, TestPriority(0)]
        public async Task TestCreateMedicalRoom()
        {
            string request = "https://localhost:7244/api/MedicalRoom";
            CreateMedicalRoomDto mdDto = new(Address1);
            CreateMedicalRoomDto mdDto2 = new(Address2);

            var content = new StringContent(JsonConvert.SerializeObject(mdDto), Encoding.UTF8, "application/json");
            var content2 = new StringContent(JsonConvert.SerializeObject(mdDto2), Encoding.UTF8, "application/json");
            var res = await _client.PostAsync(request, content);
            var res2 = await _client.PostAsync(request, content2);
            Assert.Equal(HttpStatusCode.OK, res.StatusCode);
            Assert.Equal(HttpStatusCode.OK, res2.StatusCode);
            var jsonString = await res.Content.ReadAsStringAsync();
            var jsonString2 = await res2.Content.ReadAsStringAsync();
            var cont = JsonConvert.DeserializeObject<DisplayMedicalRoomDto>(jsonString);
            var cont2 = JsonConvert.DeserializeObject<DisplayMedicalRoomDto>(jsonString2);
            Assert.NotNull(cont);
            Assert.NotNull(cont2);
            DisplayMedicalRoomDto expectedObject = new(cont.Id, mdDto.Adress);
            DisplayMedicalRoomDto expectedObject2 = new(cont2.Id, mdDto2.Adress);
            Assert.True(expectedObject.Equals(cont));
            Assert.True(expectedObject2.Equals(cont2));
            MedicalRoom? medicalRoom1 = await databaseFixture.DatabaseContext.MedicalRooms.FindAsync(cont.Id);
            MedicalRoom? medicalRoom2 = await databaseFixture.DatabaseContext.MedicalRooms.FindAsync(cont2.Id);
            Assert.NotNull(medicalRoom1);
            Assert.NotNull(medicalRoom2);
            Assert.Equal(medicalRoom1.Adress, mdDto.Adress);
            Assert.Equal(medicalRoom1.Id, cont.Id);
            Assert.Equal(medicalRoom2.Adress, mdDto2.Adress);
            Assert.Equal(medicalRoom2.Id, cont2.Id);

        }

        [Fact, TestPriority(1)]
        public async Task TestGetMedicalRooms()
        {
            Init();
            string request = "https://localhost:7244/api/MedicalRoom";
            CreateMedicalRoomDto mdDto = new(Address1);
            CreateMedicalRoomDto mdDto2 = new(Address2);

            var content = new StringContent(JsonConvert.SerializeObject(mdDto), Encoding.UTF8, "application/json");
            var content2 = new StringContent(JsonConvert.SerializeObject(mdDto2), Encoding.UTF8, "application/json");
            var res = await _client.PostAsync(request, content);
            var res2 = await _client.PostAsync(request, content2);
            Assert.Equal(HttpStatusCode.OK, res.StatusCode);
            Assert.Equal(HttpStatusCode.OK, res2.StatusCode);
            request = "https://localhost:7244/api/MedicalRoom";
            res = await _client.GetAsync(request);

            var jsonString = await res.Content.ReadAsStringAsync();
            var cont = JsonConvert.DeserializeObject<List<DisplayMedicalRoomDto>>(jsonString);
            Assert.NotNull(cont);
            Assert.True(cont.Count() >= 2);
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
