using Microsoft.AspNetCore.Mvc.Testing;
using MyDoctor.API.DTOs;
using MyDoctor.API.Controllers;
using MyDoctor.IntegTests.Helpers;
using MyDoctor.IntegTests.Orderers;
using Newtonsoft.Json;
using System.Text;
using System.Net;
using MyDoctor.Domain.Models;

namespace MyDoctor.IntegTests
{
    [TestCaseOrderer("MyDoctor.IntegTests.Orderers.PriorityOrderer", "MyDoctor.IntegTests")]
    public class MedicalRoomControllerTest : IClassFixture<DatabaseFixture>
    {
        private readonly HttpClient _client;
        private DatabaseFixture databaseFixture;
        private string Address1 = "Ion Putulai 5";
        private string Address2 = "Ana Anastasie 32154";

        public MedicalRoomControllerTest(DatabaseFixture databaseFixture)
        {
            var app = new WebApplicationFactory<MedicalRoomController>()
                .WithWebHostBuilder(builder => { });
            _client = app.CreateClient();
            this.databaseFixture = databaseFixture;
        }


        private void Init()
        {

        }

        [Fact, TestPriority(0)]
        public async Task TestCreateMedicalRoom()
        {

            // Given
            Init();

            // When
            string request = "https://localhost:7244/api/MedicalRoom";
            CreateMedicalRoomDto mdDto = new CreateMedicalRoomDto();
            mdDto.Adress = this.Address1;
            CreateMedicalRoomDto mdDto2 = new CreateMedicalRoomDto();
            mdDto2.Adress = this.Address2;

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
            DisplayMedicalRoomDto expectedObject = new DisplayMedicalRoomDto(cont.Id, mdDto.Adress);
            DisplayMedicalRoomDto expectedObject2 = new DisplayMedicalRoomDto(cont2.Id, mdDto2.Adress);
            Assert.True(expectedObject.Equals(cont));
            Assert.True(expectedObject2.Equals(cont2));
            MedicalRoom medicalRoom1 = databaseFixture.DatabaseContext.MedicalRooms.Find(cont.Id);
            MedicalRoom medicalRoom2 = databaseFixture.DatabaseContext.MedicalRooms.Find(cont2.Id);
            Assert.Equal(medicalRoom1.Adress, mdDto.Adress);
            Assert.Equal(medicalRoom1.Id, cont.Id);
            Assert.Equal(medicalRoom2.Adress, mdDto2.Adress);
            Assert.Equal(medicalRoom2.Id, cont2.Id);

        }

        [Fact, TestPriority(1)]
        public async Task TestGetMedicalRooms()
        {
            string request = "https://localhost:7244/api/MedicalRoom";
            var res = await _client.GetAsync(request);

            var jsonString = await res.Content.ReadAsStringAsync();
            var cont = JsonConvert.DeserializeObject<List<DisplayMedicalRoomDto>>(jsonString);
            Assert.True(cont.Count() == 2);
            cont.ForEach(dto => Assert.True(dto.Adress == this.Address2 || dto.Adress == this.Address1));
        }
    }
}
