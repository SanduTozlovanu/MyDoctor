using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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
        public ProcedureController(IRepository<Procedure> procedureRepository)
        {
            this.procedureRepository = procedureRepository;
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(procedureRepository.All());
        }
    }
}
