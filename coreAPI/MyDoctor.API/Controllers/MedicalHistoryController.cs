using Microsoft.AspNetCore.Mvc;
using MyDoctor.API.DTOs;
using MyDoctorApp.Domain.Models;
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
        public async Task<IActionResult> Get()
        {
            return Ok((await medicalHistoryRepository.AllAsync()).Select(mh => medicalHistoryRepository.GetMapper().Map<DisplayMedicalHistoryDto>(mh)));
        }
    }
}
