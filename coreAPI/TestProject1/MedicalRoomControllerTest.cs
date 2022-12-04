using Microsoft.AspNetCore.Mvc.Testing;
using MyDoctor.API.Controllers;
using MyDoctor.API.DTOs;
using MyDoctor.Domain.Models;
using MyDoctor.IntegTests.Helpers;
using MyDoctor.IntegTests.Orderers;
using MyDoctorApp.Infrastructure;
using Newtonsoft.Json;
using System.Text;

namespace MyDoctor.IntegTests
{
    public class MedicalRoomControllerTest : IClassFixture<DatabaseFixture>
    {
        private readonly HttpClient _client;
        private readonly DatabaseContext dbContext;
        private readonly DatabaseFixture databaseFixture;

        public MedicalRoomControllerTest(DatabaseFixture databaseFixture)
        {
            var app = new WebApplicationFactory<MedicalRoom>()
                .WithWebHostBuilder(builder => { });
            _client = app.CreateClient();
            this.databaseFixture = databaseFixture;
        }


        private void Init()
        {

        }

        [Fact, TestPriority(0)]
        public async Task CreatePacient()
        {

            // Given
            Init();

            // When
            string request = "https://localhost:7244/api/MedicalRoom";
            CreateMedicalRoomDto mdDto = new CreateMedicalRoomDto();
            mdDto.Adress = "Ion Putulai 45";

            var content = new StringContent(JsonConvert.SerializeObject(mdDto), Encoding.UTF8, "application/json");
            var res = await _client.PostAsync(request, content);
            var jsonString = await res.Content.ReadAsStringAsync();
            var cont = JsonConvert.DeserializeObject<DisplayMedicalRoomDto>(jsonString);
            DisplayMedicalRoomDto expectedObject = new DisplayMedicalRoomDto(cont.Id, mdDto.Adress);
            Assert.True(expectedObject.Equals(cont));
            MedicalRoom medicalRoom = dbContext.MedicalRooms.Find(cont.Id);
            Assert.NotNull(medicalRoom);
            Assert.Equal(medicalRoom.Adress, mdDto.Adress);
            Assert.Equal(medicalRoom.Id, cont.Id);
            
        }

        [Fact, TestPriority(1)]
        public async Task GetPacients()
        {

            // Given
            Init();

            // When
            string request = "https://localhost:7244/api/MedicalRoom";
            CreateMedicalRoomDto mdDto1 = new CreateMedicalRoomDto();
            mdDto1.Adress = "Arahidei 19";
            CreateMedicalRoomDto mdDto2 = new CreateMedicalRoomDto();
            mdDto2.Adress = "Chisinaului 290";

            var content1 = new StringContent(JsonConvert.SerializeObject(mdDto1), Encoding.UTF8, "application/json");
            var content2 = new StringContent(JsonConvert.SerializeObject(mdDto2), Encoding.UTF8, "application/json");
            var res1 = await _client.PostAsync(request, content1);
            var res2 = await _client.PostAsync(request, content2);
            Assert.Equal(System.Net.HttpStatusCode.OK, res1.StatusCode);
            Assert.Equal(res2.StatusCode, System.Net.HttpStatusCode.OK);

            var res = await _client.GetAsync(request);

            var jsonString = await res.Content.ReadAsStringAsync();
            var cont = JsonConvert.DeserializeObject<List<DisplayMedicalRoomDto>>(jsonString);
            Assert.True(cont.Count() == 2);
            cont.ForEach(dto => Assert.True(dto.Adress == mdDto1.Adress || dto.Adress == mdDto2.Adress));
        }
    }
}
