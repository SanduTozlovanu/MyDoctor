using Microsoft.AspNetCore.Mvc;
using MyDoctor.API.DTOs;
using MyDoctorApp.Domain.Models;
using MyDoctorApp.Infrastructure.Generics;

namespace MyDoctor.API.Controllers
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("1.0")]
    public class DrugsController : ControllerBase
    {
        public const string DrugStockNotFoundError = "Could not find a drugStock with this Id.";
        public const string DoctorNotFoundError = "Could not find a doctor with this Id.";
        public const string DrugNotFoundError = "Could not find a drug with this Id.";
        private readonly IRepository<Drug> drugRepository;
        private readonly IRepository<DrugStock> drugStockRepository;
        private readonly IRepository<Doctor> doctorRepository;

        public DrugsController(IRepository<Drug> drugRepository, IRepository<Doctor> doctorRepository,
            IRepository<DrugStock> drugStockRepository)
        {
            this.drugRepository = drugRepository;
            this.drugStockRepository = drugStockRepository;
            this.doctorRepository = doctorRepository;
        }
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok((await drugRepository.AllAsync()).Select(d => drugRepository.GetMapper().Map<DisplayDrugDto>(d)));
        }

        [HttpGet("{doctorId:guid}")]
        public async Task<IActionResult> GetDoctorDrugs(Guid doctorId)
        {
            var doctor = await doctorRepository.GetAsync(doctorId);
            if (doctor == null) 
            {
                return NotFound(DoctorNotFoundError);
            }
            var drugStock = (await drugStockRepository.AllAsync()).Where(ds => ds.MedicalRoomId == doctor.MedicalRoomId).FirstOrDefault();
            if (drugStock == null)
            {
                return new StatusCodeResult(500);
            }
            return Ok((await drugRepository.AllAsync()).Where(d => d.DrugStockId == drugStock.Id).Select(d => drugRepository.GetMapper().Map<DisplayDrugDto>(d)));

        }
        /// <remarks>
        /// Parameters remarks
        /// 
        ///     You can put multiple drugs.
        ///         
        /// </remarks>
        [HttpPost("{doctorId:guid}")]
        public async Task<IActionResult> Create(Guid doctorId, [FromBody] List<CreateDrugDto> dtos)
        {
            var doctor = await doctorRepository.GetAsync(doctorId);
            if (doctor == null)
            {
                return NotFound(DoctorNotFoundError);
            }
            var drugStock = (await drugStockRepository.AllAsync()).Where(ds => ds.MedicalRoomId == doctor.MedicalRoomId).FirstOrDefault();
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

        [HttpDelete("{drugId:guid}")]
        public async Task<IActionResult> DeleteDrug(Guid drugId)
        {
            try
            {
                await drugRepository.Delete(drugId);
            }
            catch (ArgumentException)
            {
                return BadRequest(DrugNotFoundError);
            }

            await drugRepository.SaveChangesAsync();
            return Ok();
        }
        [HttpPut("{drugId:guid}")]
        public async Task<IActionResult> Update(Guid drugId, [FromBody] CreateDrugDto dto)
        {
            var drug = await drugRepository.GetAsync(drugId);
            if (drug == null)
            {
                return NotFound(DrugNotFoundError);
            }

            var drugNew = new Drug(dto.Name, dto.Description, dto.Price, dto.Quantity);

            drug.Update(drugNew);

            drug = drugRepository.Update(drug);

            await drugRepository.SaveChangesAsync();
            return Ok(drugRepository.GetMapper().Map<DisplayDrugDto>(drug));
        }
    }
}
