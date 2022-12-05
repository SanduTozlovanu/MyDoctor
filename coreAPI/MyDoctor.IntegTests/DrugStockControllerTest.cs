using Microsoft.AspNetCore.Mvc.Testing;
using MyDoctor.API.DTOs;
using MyDoctor.IntegTests.Helpers;
using MyDoctor.IntegTests.Orderers;
using MyDoctor.API.Controllers;
using Newtonsoft.Json;
using System.Text;

namespace MyDoctor.IntegTests
{
    [TestCaseOrderer("MyDoctor.IntegTests.Orderers.PriorityOrderer", "MyDoctor.IntegTests")]
    public class DrugStockControllerTest : IClassFixture<DatabaseFixture>
    {
        private readonly HttpClient _client;
        private DatabaseFixture databaseFixture;

        public DrugStockControllerTest(DatabaseFixture databaseFixture)
        {
            var app = new WebApplicationFactory<DrugStockController>()
                .WithWebHostBuilder(builder => { });
            _client = app.CreateClient();
            this.databaseFixture = databaseFixture;
        }

        [Fact, TestPriority(0)]
        public async Task TestGetDrugStocks()
        {

            // When
            string request2 = "https://localhost:7244/api/DrugStock";
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
            Assert.Equal(System.Net.HttpStatusCode.OK, res2.StatusCode);

            var jsonString1 = await res1.Content.ReadAsStringAsync();
            var con1 = JsonConvert.DeserializeObject<DisplayMedicalRoomDto>(jsonString1);
            DisplayMedicalRoomDto medRoom1 = new DisplayMedicalRoomDto(con1.Id, mdDto1.Adress);

            var jsonString2 = await res2.Content.ReadAsStringAsync();
            var con2 = JsonConvert.DeserializeObject<DisplayMedicalRoomDto>(jsonString2);
            DisplayMedicalRoomDto medRoom2 = new DisplayMedicalRoomDto(con2.Id, mdDto2.Adress);

            var res = await _client.GetAsync(request2);

            var jsonString = await res.Content.ReadAsStringAsync();
            var cont = JsonConvert.DeserializeObject<List<DisplayDrugStockDto>>(jsonString);
            Assert.True(cont.Count() == 2);
            uint foundTimes = 0;
            cont.ForEach(dto => {
                if(dto.MedicalRoomId == medRoom1.Id || dto.MedicalRoomId == medRoom2.Id)
                    foundTimes++;
                });
            Assert.True(foundTimes == 2);
        }
    }
}
