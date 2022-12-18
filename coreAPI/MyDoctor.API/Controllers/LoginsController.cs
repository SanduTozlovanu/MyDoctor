using Microsoft.AspNetCore.Mvc;
using MyDoctor.API.DTOs;
using MyDoctor.API.Helpers;
using MyDoctorApp.Domain.Models;
using MyDoctorApp.Infrastructure.Generics;

namespace MyDoctor.API.Controllers
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("1.0")]
    public class LoginsController : ControllerBase
    {
        public const string InvalidCredentialsError = "Invalid credentials!";
        private readonly IRepository<Patient> patientsRepository;
        private readonly IRepository<Doctor> doctorsRepository;

        public LoginsController(IRepository<Patient> patientsRepository,
            IRepository<Doctor> doctorsRepository)
        {
            this.patientsRepository = patientsRepository;
            this.doctorsRepository = doctorsRepository;
        }

        [HttpPost]
        public async Task<IActionResult> Login([FromBody] LoginDto dto)
        {
            User? user = (await patientsRepository.FindAsync(p => p.Email == dto.Email)).FirstOrDefault();
            user ??= (await doctorsRepository.FindAsync(d => d.Email == dto.Email)).FirstOrDefault();

            if (user != null && AccountInfoManager.ValidatePassword(user.Password, dto.Password))
            {
                string jwtToken = JwtManager.GenerateToken(user);
                return Ok(new DisplayLoginDto(user, jwtToken));
            }

            return BadRequest(InvalidCredentialsError);
        }
    }
}
