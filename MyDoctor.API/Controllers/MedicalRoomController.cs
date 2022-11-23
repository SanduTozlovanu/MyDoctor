using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyDoctor.Domain.Models;
using MyDoctorApp.Infrastructure.Generics;
using MyDoctorApp.Infrastructure.Generics.GenericRepositories;

namespace MyDoctor.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MedicalRoomController : ControllerBase
    {
        private readonly IRepository<MedicalRoom> medicalRoomRepository;

        public MedicalRoomController(IRepository<MedicalRoom> medicalRoomRepository)
        {
            this.medicalRoomRepository = medicalRoomRepository;
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(medicalRoomRepository.All());
        }
    }
}
