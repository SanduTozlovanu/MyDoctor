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
        public async Task<IActionResult> Get()
        {
            return Ok((await drugRepository.AllAsync()).Select(d => drugRepository.GetMapper().Map<DisplayDrugDto>(d)));
        }
        /// <remarks>
        /// Parameters remarks
        /// 
        ///     You can put multiple drugs.
        ///         
        /// </remarks>
        [HttpPost("{drugStockId:guid}")]
        public async Task<IActionResult> Create(Guid drugStockId, [FromBody] List<CreateDrugDto> dtos)
        {
            var drugStock = await drugStockRepository.GetAsync(drugStockId);
            if (drugStock == null)
            {
                return NotFound(DrugStockNotFoundError);
            }

            List<Drug> drugs = dtos.Select(dto => drugRepository.GetMapper().Map<Drug>(dto)).ToList();
            List<Guid> drugsIds = new();
            drugs.ForEach(drug => drugsIds.Add(drug.Id));
            drugStock.RegisterDrugsToDrugStock(drugs);

            drugs.ForEach(async d => await drugRepository.AddAsync(d));
            await drugRepository.SaveChangesAsync();
            await drugStockRepository.SaveChangesAsync();
            List<DisplayDrugDto> drugDtos = new();
            drugs.ForEach(drug => drugDtos.Add(drugRepository.GetMapper().Map<DisplayDrugDto>(drug)));
            return Ok(drugDtos);
        }

    }
}
