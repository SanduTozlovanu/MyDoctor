using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyDoctor.Domain.Models;
using MyDoctorApp.Infrastructure.Generics;
using MyDoctorApp.Infrastructure.Generics.GenericRepositories;

namespace MyDoctor.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HospitalAdmissionFileController : ControllerBase
    {
        private readonly IRepository<HospitalAdmissionFile> hospitalAdmissioFileRepository;

        public HospitalAdmissionFileController(IRepository<HospitalAdmissionFile> hospitalAdmissioFileRepository)
        {
            this.hospitalAdmissioFileRepository = hospitalAdmissioFileRepository;
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(hospitalAdmissioFileRepository.All());
        }
    }
}
