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
    public class PatientController : ControllerBase
    {
        public const string UsedEmailError = "The email is already used!";
        public const string InvalidEmailError = "The email is invalid!";
        public const string BigAgeError = "Too big age value.";
        private readonly IRepository<Patient> patientRepository;
        private readonly IRepository<MedicalHistory> medicalHistoryRepository;
        private readonly IRepository<Doctor> doctorRepository;

        public PatientController(IRepository<Patient> patientRepository,
            IRepository<MedicalHistory> medicalHistoryRepository,
            IRepository<Doctor> doctorRepository)
        {
            this.patientRepository = patientRepository;
            this.medicalHistoryRepository = medicalHistoryRepository;
            this.doctorRepository = doctorRepository;
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(patientRepository.All().Select(p => new DisplayPatientDto(p.Id, p.FirstName, p.LastName, p.Email, p.Age)));
        }

        [HttpPost]
        public IActionResult Create([FromBody] CreatePatientDto dto)
        {
            if (dto.Age > 120) return BadRequest(BigAgeError);

            var oldPatient = patientRepository.Find(p => p.Email == dto.UserDetails.Email).FirstOrDefault();
            var oldDoctor = doctorRepository.Find(d => d.Email == dto.UserDetails.Email).FirstOrDefault();
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
            patientRepository.Add(patient);

            medicalHistoryRepository.SaveChanges();
            patientRepository.SaveChanges();
            return Ok(new DisplayPatientDto(patient.Id, patient.FirstName, patient.LastName, patient.Email, patient.Age));
        }



        [HttpPut("{patientId:guid}")]
        public IActionResult Update(Guid patientId, [FromBody] CreatePatientDto dto)
        {
            var patient = patientRepository.Get(patientId);
            if (patient == null)
            {
                return NotFound();
            }

            if (dto.Age > 120) return BadRequest(BigAgeError);

            var oldPatient = patientRepository.Find(p => p.Email == dto.UserDetails.Email).FirstOrDefault();
            var oldDoctor = doctorRepository.Find(d => d.Email == dto.UserDetails.Email).FirstOrDefault();
            if (oldPatient != null || oldDoctor != null)
            {
                return BadRequest(UsedEmailError);
            }

            if (!AccountInfoManager.ValidateEmail(dto.UserDetails.Email))
            {
                return BadRequest(InvalidEmailError);
            }

            string hashedPassword = AccountInfoManager.HashPassword(dto.UserDetails.Password);
            var newPatient = new Patient(dto.UserDetails.Email, hashedPassword, dto.UserDetails.FirstName, dto.UserDetails.LastName, dto.Age);

            patient.Update(newPatient);

            patientRepository.Update(patient);

            patientRepository.SaveChanges();
            return Ok();
        }

        [HttpDelete("{patientId:guid}")]
        public IActionResult Delete(Guid patientId)
        {
            var patient = patientRepository.Get(patientId);
            if (patient == null)
            {
                return NotFound();
            }

            patientRepository.Delete(patient);

            patientRepository.SaveChanges();
            return Ok();
        }
    }
}
