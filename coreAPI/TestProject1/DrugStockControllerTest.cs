using Microsoft.AspNetCore.Mvc.Testing;
using MyDoctor.API.DTOs;
using Newtonsoft.Json;
using System.Text;

namespace MyDoctor.IntegTests
{
    public class DrugStockControllerTest : AIntegrationTest
    {
        public DrugStockControllerTest(WebApplicationFactory<Program> factory) : base(factory)
        {
        }

        [Fact]
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
            cont.ForEach(dto => Assert.True(dto.MedicalRoomId == medRoom1.Id || dto.MedicalRoomId == medRoom2.Id));
        }
    }
}
