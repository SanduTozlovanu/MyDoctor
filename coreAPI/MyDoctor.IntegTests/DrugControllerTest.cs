using Microsoft.AspNetCore.Mvc.Testing;
using MyDoctor.API.Controllers;
using MyDoctor.API.DTOs;
using MyDoctor.IntegTests.Helpers;
using MyDoctor.IntegTests.Orderers;
using Newtonsoft.Json;
using System.Net;
using System.Text;

namespace MyDoctor.IntegTests
{

    [TestCaseOrderer("MyDoctor.IntegTests.Orderers.PriorityOrderer", "MyDoctor.IntegTests")]
    public class DrugControllerTest : IClassFixture<DatabaseFixture>
    {
        private readonly HttpClient _client;
        private DatabaseFixture databaseFixture;
        private void Init()
        {

        }
        public DrugControllerTest(DatabaseFixture databaseFixture)
        {
            var app = new WebApplicationFactory<DrugController>()
                .WithWebHostBuilder(builder => { });
            _client = app.CreateClient();
            this.databaseFixture = databaseFixture;
        }

        [Fact, TestPriority(0)]
        public async Task TestGetDrugs()
        {
            Init();

            // When
            string request = "https://localhost:7244/api/MedicalRoom";
            string request1 = "https://localhost:7244/api/DrugStock";
            string request3 = "https://localhost:7244/api/Drug/{0}";
            CreateMedicalRoomDto mdDto1 = new CreateMedicalRoomDto();
            mdDto1.Adress = "Strada Farmaciei 8";

            var content1 = new StringContent(JsonConvert.SerializeObject(mdDto1), Encoding.UTF8, "application/json");
            var res1 = await _client.PostAsync(request, content1);

            Assert.Equal(System.Net.HttpStatusCode.OK, res1.StatusCode);

            var jsonString1 = await res1.Content.ReadAsStringAsync();
            var con1 = JsonConvert.DeserializeObject<DisplayMedicalRoomDto>(jsonString1);
            DisplayMedicalRoomDto medRoom1 = new DisplayMedicalRoomDto(con1.Id, mdDto1.Adress);

            var res = await _client.GetAsync(request1);

            var jsonString = await res.Content.ReadAsStringAsync();
            var cont = JsonConvert.DeserializeObject<List<DisplayDrugStockDto>>(jsonString);
            Assert.True(cont.Count() == 1);
            cont.ForEach(dto => Assert.True(dto.MedicalRoomId == medRoom1.Id));
            DisplayDrugStockDto drugStock = cont.First();

            CreateDrugDto drugDto1 = new CreateDrugDto();
            drugDto1.Description = "Peniciline is bad";
            drugDto1.Name = "Not Peniciline";
            drugDto1.Price = 35.64;
            drugDto1.Quantity = 5;

            CreateDrugDto drugDto2 = new CreateDrugDto();
            drugDto2.Description = "Peniciline is good";
            drugDto2.Name = "Peniciline";
            drugDto2.Price = 25.48;
            drugDto2.Quantity = 2;

            List<CreateDrugDto> dtos = new List<CreateDrugDto>();
            dtos.Add(drugDto1);
            dtos.Add(drugDto2);
            var content = new StringContent(JsonConvert.SerializeObject(dtos), Encoding.UTF8, "application/json");

            var result = await _client.PostAsync(String.Format(request3,drugStock.Id.ToString()), content);
            var jsonString2 = await result.Content.ReadAsStringAsync();
            var displayDtos = JsonConvert.DeserializeObject<List<DisplayDrugDto>>(jsonString2);
            Assert.Equal(displayDtos.Count, 2);
            displayDtos.ForEach(dto => Assert.True(dto.Equals(new DisplayDrugDto
                (dto.Id, drugStock.Id, drugDto1.Name, drugDto1.Description, drugDto1.Price, drugDto1.Quantity))
                 || dto.Equals(new DisplayDrugDto
                (dto.Id, drugStock.Id, drugDto2.Name, drugDto2.Description, drugDto2.Price, drugDto2.Quantity))));

        }
        [Fact, TestPriority(1)]
        public async Task TestGetDrugs_WrongDrugStockId()
        {
            string request3 = "https://localhost:7244/api/Drug/{0}";

            CreateDrugDto drugDto1 = new CreateDrugDto();
            drugDto1.Description = "Peniciline is bad";
            drugDto1.Name = "Not Peniciline";
            drugDto1.Price = 35.64;
            drugDto1.Quantity = 5;

            CreateDrugDto drugDto2 = new CreateDrugDto();
            drugDto2.Description = "Peniciline is good";
            drugDto2.Name = "Peniciline";
            drugDto2.Price = 25.48;
            drugDto2.Quantity = 2;

            List<CreateDrugDto> dtos = new List<CreateDrugDto>();
            dtos.Add(drugDto1);
            dtos.Add(drugDto2);
            var content = new StringContent(JsonConvert.SerializeObject(dtos), Encoding.UTF8, "application/json");

            var result = await _client.PostAsync(String.Format(request3, "48db124a-e731-4664-9add-44172a403b90"), content);
            var jsonString1 = await result.Content.ReadAsStringAsync();
            Assert.Equal(HttpStatusCode.NotFound, result.StatusCode);
            Assert.Equal(DrugController.DrugStockNotFoundError, jsonString1);
        }
    }
}
