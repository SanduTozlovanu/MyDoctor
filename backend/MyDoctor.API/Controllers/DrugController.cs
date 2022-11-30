using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyDoctor.API.Dtos;
using MyDoctor.API.DTOs;
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
            return Ok(drugRepository.All().Select(d => new DisplayDrugDto(d.Id, d.DrugStockId, d.Name, d.Description, d.Price, d.Quantity)));
        }

        [HttpPost]
        public IActionResult Create(Guid drugStockId, [FromBody] List<CreateDrugDto> dtos)
        {
            var drugStock = drugStockRepository.Get(drugStockId);
            if (drugStock == null)
            {
                return NotFound("Could not find a drugStock with this Id.");
            }

            List<Drug> drugs = dtos.Select(dto => new Drug(dto.Name, dto.Description, dto.Price, dto.Quantity)).ToList();
            List<Guid> drugsIds = new List<Guid>();
            drugs.ForEach(drug => drugsIds.Add(drug.Id));
            drugStock.RegisterDrugsToDrugStock(drugs);

            drugs.ForEach(d => drugRepository.Add(d));
            drugRepository.SaveChanges();
            drugStockRepository.SaveChanges();

            return Ok(new { drugsIds = drugsIds });
        }
    }
}
