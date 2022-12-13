using MyDoctor.API.Controllers;
using MyDoctor.API.DTOs;
using MyDoctor.Tests.Helpers;
using Newtonsoft.Json;
using System.Net;
using System.Text;

namespace MyDoctor.Tests.IntegTests
{
    public class DrugControllerTest : BaseControllerTest<DrugController>
    {
        public DrugControllerTest(CustomWebApplicationFactory<Program> factory) : base(factory)
        {
        }

        private void Init()
        {

        }

        [Fact]
        public async Task TestGetDrugs()
        {
            Init();

            // When
            string request = "https://localhost:7244/api/MedicalRoom";
            string request1 = "https://localhost:7244/api/DrugStock";
            string request3 = "https://localhost:7244/api/Drug/{0}";
            CreateMedicalRoomDto mdDto1 = new("Strada Farmaciei 8");

            var content1 = new StringContent(JsonConvert.SerializeObject(mdDto1), Encoding.UTF8, "application/json");
            var res1 = await HttpClient.PostAsync(request, content1);

            Assert.Equal(HttpStatusCode.OK, res1.StatusCode);

            var jsonString1 = await res1.Content.ReadAsStringAsync();
            var cont1 = JsonConvert.DeserializeObject<DisplayMedicalRoomDto>(jsonString1);
            Assert.NotNull(cont1);
            DisplayMedicalRoomDto medRoom1 = new(cont1.Id, mdDto1.Adress);

            var res = await HttpClient.GetAsync(request1);

            var jsonString = await res.Content.ReadAsStringAsync();
            var cont = JsonConvert.DeserializeObject<List<DisplayDrugStockDto>>(jsonString);
            Assert.NotNull(cont);
            Assert.True(cont.Count() >= 1);
            bool medicalRoomFound = false;
            cont.ForEach(dto =>
            {
                if (dto.MedicalRoomId == medRoom1.Id)
                    medicalRoomFound = true;
            });
            Assert.True(medicalRoomFound);
            DisplayDrugStockDto drugStock = cont.First();

            CreateDrugDto drugDto1 = new("Peniciline is bad", "Not Peniciline", 35.64, 5);

            CreateDrugDto drugDto2 = new("Peniciline is good", "Peniciline", 25.48, 2);

            List<CreateDrugDto> dtos = new();
            dtos.Add(drugDto1);
            dtos.Add(drugDto2);
            var content = new StringContent(JsonConvert.SerializeObject(dtos), Encoding.UTF8, "application/json");

            var result = await HttpClient.PostAsync(string.Format(request3, drugStock.Id.ToString()), content);
            var jsonString2 = await result.Content.ReadAsStringAsync();
            var displayDtos = JsonConvert.DeserializeObject<List<DisplayDrugDto>>(jsonString2);
            Assert.NotNull(displayDtos);
            Assert.Equal(2, displayDtos.Count);
            displayDtos.ForEach(dto => Assert.True(dto.Equals(new DisplayDrugDto
                (dto.Id, drugStock.Id, drugDto1.Name, drugDto1.Description, drugDto1.Price, drugDto1.Quantity))
                 || dto.Equals(new DisplayDrugDto
                (dto.Id, drugStock.Id, drugDto2.Name, drugDto2.Description, drugDto2.Price, drugDto2.Quantity))));

        }
        [Fact]
        public async Task TestGetDrugs_WrongDrugStockId()
        {
            string request3 = "https://localhost:7244/api/Drug/{0}";

            CreateDrugDto drugDto1 = new("Peniciline is bad", "Not Peniciline", 35.64, 5);

            CreateDrugDto drugDto2 = new("Peniciline is good", "Peniciline", 25.48, 2);

            List<CreateDrugDto> dtos = new();
            dtos.Add(drugDto1);
            dtos.Add(drugDto2);
            var content = new StringContent(JsonConvert.SerializeObject(dtos), Encoding.UTF8, "application/json");

            var result = await HttpClient.PostAsync(string.Format(request3, "48db124a-e731-4664-9add-44172a403b90"), content);
            var jsonString1 = await result.Content.ReadAsStringAsync();
            Assert.Equal(HttpStatusCode.NotFound, result.StatusCode);
            Assert.Equal(DrugController.DrugStockNotFoundError, jsonString1);
        }
    }
}
