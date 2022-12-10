using Microsoft.AspNetCore.Mvc;
using MyDoctor.API.DTOs;
using MyDoctorApp.Domain.Models;
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
            return Ok(drugRepository.All().Select(d => drugRepository.GetMapper().Map<DisplayDrugDto>(d)));
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

            List<Drug> drugs = dtos.Select(dto => drugRepository.GetMapper().Map<Drug>(dto)).ToList();
            List<Guid> drugsIds = new List<Guid>();
            drugs.ForEach(drug => drugsIds.Add(drug.Id));
            drugStock.RegisterDrugsToDrugStock(drugs);

            drugs.ForEach(d => drugRepository.Add(d));
            drugRepository.SaveChanges();
            drugStockRepository.SaveChanges();
            List<DisplayDrugDto> drugDtos = new List<DisplayDrugDto>();
            drugs.ForEach(drug => drugDtos.Add(drugRepository.GetMapper().Map<DisplayDrugDto>(drug)));
            return Ok(drugDtos);
        }

    }
}
