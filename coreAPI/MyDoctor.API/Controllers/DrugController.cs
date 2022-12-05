using Microsoft.AspNetCore.Mvc;
using MyDoctor.API.DTOs;
using MyDoctor.Domain.Models;
using MyDoctorApp.Infrastructure.Generics;

namespace MyDoctor.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DrugController : ControllerBase
    {
        public const string DrugStockNotFoundError = "Could not find a drugStock with this Id.";
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
        /// <remarks>
        /// Parameters remarks
        /// 
        ///     You can put multiple drugs.
        ///         
        /// </remarks>
        [HttpPost("{drugStockId:guid}")]
        public IActionResult Create(Guid drugStockId, [FromBody] List<CreateDrugDto> dtos)
        {
            var drugStock = drugStockRepository.Get(drugStockId);
            if (drugStock == null)
            {
                return NotFound(DrugStockNotFoundError);
            }

            List<Drug> drugs = dtos.Select(dto => new Drug(dto.Name, dto.Description, dto.Price, dto.Quantity)).ToList();
            List<Guid> drugsIds = new List<Guid>();
            drugs.ForEach(drug => drugsIds.Add(drug.Id));
            drugStock.RegisterDrugsToDrugStock(drugs);

            drugs.ForEach(d => drugRepository.Add(d));
            drugRepository.SaveChanges();
            drugStockRepository.SaveChanges();
            List<DisplayDrugDto> drugDtos = new List<DisplayDrugDto>();
            drugs.ForEach(drug => drugDtos.Add(new DisplayDrugDto(drug.Id, drug.DrugStockId, drug.Name, drug.Description, drug.Price, drug.Quantity)));
            return Ok(drugDtos);
        }

    }
}
