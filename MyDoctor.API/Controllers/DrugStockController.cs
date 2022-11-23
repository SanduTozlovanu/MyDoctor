using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyDoctor.API.Dtos;
using MyDoctor.Domain.Models;
using MyDoctorApp.Infrastructure.Generics;
using MyDoctorApp.Infrastructure.Generics.GenericRepositories;

namespace MyDoctor.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DrugStockController : ControllerBase
    {
        private readonly IRepository<DrugStock> drugStockRepository;
        private readonly IRepository<Drug> drugRepository;

        public DrugStockController(IRepository<DrugStock> drugStockRepository)
        {
            this.drugStockRepository = drugStockRepository;
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(drugStockRepository.All());
        }

        [HttpPost]
        public IActionResult Create([FromBody] List<CreateDrugDto> dtos)
        {
            var drugStock = new DrugStock();
            List<Drug> drugs = dtos.Select(d => new Drug(d.Name, d.Description, d.Price, d.Quantity)).ToList();
            drugStock.RegisterDrugsToDrugStock(drugs);
            drugs.ForEach(q => drugRepository.Add(q));
            drugRepository.SaveChanges();
            return NoContent();
        }
    }
}
