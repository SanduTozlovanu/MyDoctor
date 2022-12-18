using Microsoft.AspNetCore.Mvc;
using MyDoctor.API.DTOs;
using MyDoctorApp.Domain.Models;
using MyDoctorApp.Infrastructure.Generics;

namespace MyDoctor.API.Controllers
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("1.0")]
    public class ProceduresController : ControllerBase
    {
        private readonly IRepository<Procedure> procedureRepository;

        public ProceduresController(IRepository<Procedure> procedureRepository)
        {
            this.procedureRepository = procedureRepository;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok((await procedureRepository.AllAsync()).Select(p => procedureRepository.GetMapper().Map<DisplayProcedureDto>(p)));
        }
    }
}
