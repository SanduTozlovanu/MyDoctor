using Microsoft.AspNetCore.Mvc;
using MyDoctor.API.DTOs;
using MyDoctor.Domain.Models;
using MyDoctorApp.Infrastructure.Generics;

namespace MyDoctor.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProcedureController : ControllerBase
    {
        private readonly IRepository<Procedure> procedureRepository;

        public ProcedureController(IRepository<Procedure> procedureRepository)
        {
            this.procedureRepository = procedureRepository;
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(procedureRepository.All().Select(p => new DisplayProcedureDto(p.Id, p.PrescriptionId,p.Description, p.Price, p.Name)));
        }
    }
}
