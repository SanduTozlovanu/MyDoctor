using Microsoft.AspNetCore.Mvc;
using MyDoctor.API.DTOs;
using MyDoctorApp.Domain.Models;
using MyDoctorApp.Infrastructure.Generics;

namespace MyDoctor.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SpecialityController : ControllerBase
    {
        public const string PatientNotFoundError = "Could not find a patient with this Id.";
        public const string DoctorNotFoundError = "Could not find a doctor with this Id.";
        private readonly IRepository<Speciality> specialityRepository;

        public SpecialityController(IRepository<Speciality> specialityRepository)
        { 
            this.specialityRepository = specialityRepository;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok((await specialityRepository.AllAsync()).Select(a => specialityRepository.GetMapper().Map<DisplaySpecialityDto>(a)));
        }

        [HttpPost("create_speciality")]
        public async Task<IActionResult> Create([FromBody] CreateSpecialityDto dto)
        {
            var speciality = new Speciality(dto.Name);

            await specialityRepository.AddAsync(speciality);

            await specialityRepository.SaveChangesAsync();

            return Ok(specialityRepository.GetMapper().Map<DisplaySpecialityDto>(speciality));
        }
    }


}
