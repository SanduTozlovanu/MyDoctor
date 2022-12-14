using MyDoctor.API.Controllers;
using MyDoctor.API.DTOs;
using MyDoctor.Tests.Helpers;
using Newtonsoft.Json;
using System.Text;

namespace MyDoctor.Tests.IntegTests
{
    public class DrugStockControllerTest : BaseControllerTest<DrugStockController>
    {
        public DrugStockControllerTest(CustomWebApplicationFactory<Program> factory) : base(factory)
        {
        }

        [Fact]
        public async Task TestGetDrugStocks()
        {

            // When
            string request2 = "https://localhost:7244/api/DrugStock";
            string request = "https://localhost:7244/api/MedicalRoom";
            CreateMedicalRoomDto mdDto1 = new("Arahidei 19");
            CreateMedicalRoomDto mdDto2 = new("Chisinaului 290");

            var content1 = new StringContent(JsonConvert.SerializeObject(mdDto1), Encoding.UTF8, "application/json");
            var content2 = new StringContent(JsonConvert.SerializeObject(mdDto2), Encoding.UTF8, "application/json");
            var res1 = await HttpClient.PostAsync(request, content1);
            var res2 = await HttpClient.PostAsync(request, content2);

            Assert.Equal(System.Net.HttpStatusCode.OK, res1.StatusCode);
            Assert.Equal(System.Net.HttpStatusCode.OK, res2.StatusCode);

            var jsonString1 = await res1.Content.ReadAsStringAsync();
            var con1 = JsonConvert.DeserializeObject<DisplayMedicalRoomDto>(jsonString1);
            Assert.NotNull(con1);
            DisplayMedicalRoomDto medRoom1 = new(con1.Id, mdDto1.Adress);

            var jsonString2 = await res2.Content.ReadAsStringAsync();
            var con2 = JsonConvert.DeserializeObject<DisplayMedicalRoomDto>(jsonString2);
            Assert.NotNull(con2);
            DisplayMedicalRoomDto medRoom2 = new(con2.Id, mdDto2.Adress);

            var res = await HttpClient.GetAsync(request2);

            var jsonString = await res.Content.ReadAsStringAsync();
            var cont = JsonConvert.DeserializeObject<List<DisplayDrugStockDto>>(jsonString);
            Assert.NotNull(cont);
            Assert.True(cont.Count >= 2);
            uint foundTimes = 0;
            cont.ForEach(dto =>
            {
                if (dto.MedicalRoomId == medRoom1.Id || dto.MedicalRoomId == medRoom2.Id)
                    foundTimes++;
            });
            Assert.True(foundTimes == 2);
        }
    }
}
