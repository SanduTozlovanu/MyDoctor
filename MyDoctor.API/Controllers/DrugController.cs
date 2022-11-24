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
    public class DrugController : ControllerBase
    {
        private readonly IRepository<Drug> drugRepository;
        private readonly IRepository<DrugStock> drugStockRepository;

        public DrugController(IRepository<Drug> drugRepository,
            IRepository<DrugStock> drugStockRepository)
        {
            this.drugRepository = drugRepository;
            this.drugStockRepository = drugStockRepository;
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(drugRepository.All());
        }

        [HttpPost]
        public IActionResult Create(Guid drugStockId, [FromBody] CreateDrugDto dto)
        {
            var drugStock = drugStockRepository.Get(drugStockId);
            if (drugStock == null)
            {
                return NotFound();
            }

            var drug = new Drug(dto.Name, dto.Description, dto.Price, dto.Quantity);
            drugStock.RegisterDrugsToDrugStock(new List<Drug> { drug });

            drugRepository.Add(drug);
            drugRepository.SaveChanges();
            drugStockRepository.SaveChanges();

            return NoContent();
        }
    }
}
