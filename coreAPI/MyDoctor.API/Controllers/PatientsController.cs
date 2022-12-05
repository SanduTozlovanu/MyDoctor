using Microsoft.AspNetCore.Mvc;
using MyDoctor.API.DTOs;
using MyDoctor.API.Helpers;
using MyDoctor.Domain.Models;
using MyDoctorApp.Infrastructure.Generics;

namespace MyDoctor.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PatientsController : ControllerBase
    {
        public const string UsedEmailError = "The email is already used!";
        public const string InvalidEmailError = "The email is invalid!";
        public const string BigAgeError = "Too big age value.";
        private readonly IRepository<Patient> patientsRepository;
        private readonly IRepository<MedicalHistory> medicalHistoryRepository;
        private readonly IRepository<Doctor> doctorsRepository;

        public PatientsController(IRepository<Patient> patientsRepository,
            IRepository<MedicalHistory> medicalHistoryRepository,
            IRepository<Doctor> doctorsRepository)
        {
            this.patientsRepository = patientsRepository;
            this.medicalHistoryRepository = medicalHistoryRepository;
            this.doctorsRepository = doctorsRepository;
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(patientsRepository.All().Select(p => new DisplayPatientDto(p.Id, p.FirstName, p.LastName, p.Email, p.Age)));
        }

        [HttpPost]
        public IActionResult Create([FromBody] CreatePatientDto dto)
        {
            if (dto.Age > 120) return BadRequest(BigAgeError);

            var oldPatient = patientsRepository.Find(p => p.Email == dto.UserDetails.Email).FirstOrDefault();
            var oldDoctor = doctorsRepository.Find(d => d.Email == dto.UserDetails.Email).FirstOrDefault();
            if (oldPatient != null || oldDoctor != null)
            {
                return BadRequest(UsedEmailError);
            }

            if (!AccountInfoManager.ValidateEmail(dto.UserDetails.Email))
            {
                return BadRequest(InvalidEmailError);
            }

            string hashedPassword = AccountInfoManager.HashPassword(dto.UserDetails.Password);
            var patient = new Patient(dto.UserDetails.Email, hashedPassword, dto.UserDetails.FirstName, dto.UserDetails.LastName, dto.Age);
            var medicalHistory = new MedicalHistory();

            patient.RegisterMedicalHistory(medicalHistory);

            medicalHistoryRepository.Add(medicalHistory);
            patientsRepository.Add(patient);

            medicalHistoryRepository.SaveChanges();
            patientsRepository.SaveChanges();
            return Ok(new DisplayPatientDto(patient.Id, patient.FirstName, patient.LastName, patient.Email, patient.Age));
        }
    }
}
