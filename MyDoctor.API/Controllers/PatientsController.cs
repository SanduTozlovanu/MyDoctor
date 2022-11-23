using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyDoctor.Domain.Models;
using MyDoctorApp.Infrastructure.Generics;
using MyDoctorApp.Infrastructure.Generics.GenericRepositories;

namespace MyDoctor.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PatientsController : ControllerBase
    {
        private readonly IRepository<Patient> patientsRepository;

        public PatientsController(IRepository<Patient> patientsRepository)
        {
            this.patientsRepository = patientsRepository;
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(patientsRepository.All());
        }
    }
}
