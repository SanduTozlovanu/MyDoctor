using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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
        public ProcedureController(IRepository<Procedure> procedureRepository)
        {
            this.procedureRepository = procedureRepository;
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(procedureRepository.All());
        }

        [HttpPost]
        public IActionResult Create([FromBody] CreateProcedureDto dto)
        {
            var procedure = new Procedure(dto.Name, dto.Description, dto.Price);
            procedureRepository.Add(procedure);
            procedureRepository.SaveChanges();
            return Created(nameof(Get), procedure);
        }

    }
}
