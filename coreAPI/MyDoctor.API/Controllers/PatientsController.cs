using Microsoft.AspNetCore.Mvc;
using MyDoctor.API.DTOs;
using MyDoctor.API.Helpers;
using MyDoctor.Domain.Models;
using MyDoctorApp.Infrastructure.Generics;
using MyDoctorApp.Infrastructure.Generics.GenericRepositories;
using System.ComponentModel.DataAnnotations;

namespace MyDoctor.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PatientsController : ControllerBase
    {
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
            var oldPatient = patientsRepository.Find(p => p.Email == dto.UserDetails.Email).FirstOrDefault();
            var oldDoctor = doctorsRepository.Find(d => d.Email == dto.UserDetails.Email).FirstOrDefault();
            if (oldPatient != null || oldDoctor != null)
            {
                return BadRequest("The email is already used!");
            }

            if (!AccountInfoManager.ValidateEmail(dto.UserDetails.Email))
            {
                return BadRequest("The email is invalid!");
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
