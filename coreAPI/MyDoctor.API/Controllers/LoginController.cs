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
            Patient? patient = patientsRepository.Find(p => p.Email == dto.Email).FirstOrDefault();
            Doctor? doctor = doctorsRepository.Find(d => d.Email == dto.Email).FirstOrDefault();

            if (patient != null)
            {
                if (AccountInfoManager.ValidatePassword(patient.Password, dto.Password))
                {
                    return Ok(new DisplayLoginDto(patient.Id, patient.AccountType, patient.FirstName, patient.LastName, patient.Email));
                }
            }
            else if (doctor != null)
            {
                if (AccountInfoManager.ValidatePassword(doctor.Password, dto.Password))
                {
                    return Ok(new DisplayLoginDto(doctor.Id, doctor.AccountType, doctor.FirstName, doctor.LastName, doctor.Email));
                }
            }

            return BadRequest("Invalid credentials!");
        }
    }
}
