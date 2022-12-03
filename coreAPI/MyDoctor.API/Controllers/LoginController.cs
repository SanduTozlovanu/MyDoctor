using Microsoft.AspNetCore.Mvc;
using MyDoctor.API.DTOs;
using MyDoctor.API.Helpers;
using MyDoctor.Domain.Models;
using MyDoctorApp.Infrastructure.Generics;
using MyDoctorApp.Infrastructure.Generics.GenericRepositories;

namespace MyDoctor.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly IRepository<Patient> patientsRepository;
        private readonly IRepository<Doctor> doctorsRepository;

        public LoginController(IRepository<Patient> patientsRepository,
            IRepository<Doctor> doctorsRepository)
        {
            this.patientsRepository = patientsRepository;
            this.doctorsRepository = doctorsRepository;
        }

        [HttpPost]
        public IActionResult Login([FromBody] LoginDto dto)
        {
            User? user = patientsRepository.Find(p => p.Email == dto.Email).FirstOrDefault();
            user ??= doctorsRepository.Find(d => d.Email == dto.Email).FirstOrDefault();

            if (user != null)
            {
                if (AccountInfoManager.ValidatePassword(user.Password, dto.Password))
                {
                    string jwtToken = JwtManager.GenerateToken(user);
                    return Ok(new DisplayLoginDto(user, jwtToken));
                }
            }

            return BadRequest("Invalid credentials!");
        }
    }
}
