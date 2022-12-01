using Microsoft.AspNetCore.Mvc;
using MyDoctor.API.DTOs;
using MyDoctor.Domain.Models;
using MyDoctorApp.Infrastructure.Generics;

namespace MyDoctor.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MedicalHistoryController : ControllerBase
    {
        private readonly IRepository<MedicalHistory> medicalHistoryRepository;

        public MedicalHistoryController(IRepository<MedicalHistory> medicalHistoryRepository)
        {
            this.medicalHistoryRepository = medicalHistoryRepository;
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(medicalHistoryRepository.All().Select(mh => new DisplayMedicalHistoryDto(mh.Id, mh.PatientId)));
        }
    }
}
