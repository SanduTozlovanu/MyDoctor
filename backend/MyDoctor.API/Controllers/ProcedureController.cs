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
    public class ProcedureController : ControllerBase
    {
        private readonly IRepository<Procedure> procedureRepository;
        private readonly IRepository<Prescription> prescriptionRepository;

        public ProcedureController(IRepository<Procedure> procedureRepository,
            IRepository<Prescription> prescriptionRepository)
        {
            this.procedureRepository = procedureRepository;
            this.prescriptionRepository = prescriptionRepository;
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(procedureRepository.All());
        }

        [HttpPost("{prescriptionId:guid}/create_procedures")]
        public IActionResult Create(Guid prescriptionId, [FromBody] List<CreateDrugDto> dtos)
        {
            var prescription = prescriptionRepository.Get(prescriptionId);
            if (prescription == null)
            {
                return NotFound("Could not find a prescription with this id.");
            }

            List<Procedure> procedures = dtos.Select(dto => new Procedure(dto.Name, dto.Description, dto.Price)).ToList();

            prescription.RegisterProcedures(procedures);

            procedures.ForEach(p => procedureRepository.Add(p));
            procedureRepository.SaveChanges();
            prescriptionRepository.SaveChanges();
            List<Guid> proceduresIds = new List<Guid>();
            procedures.ForEach(p => proceduresIds.Add(p.Id));
            return Ok(new { id = proceduresIds });
        }

    }
}
