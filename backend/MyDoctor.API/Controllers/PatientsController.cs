using Microsoft.AspNetCore.Mvc;
using MyDoctor.API.DTOs;
using MyDoctor.Domain.Models;
using MyDoctorApp.Infrastructure.Generics;

namespace MyDoctor.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PatientsController : ControllerBase
    {
        private readonly IRepository<Patient> patientsRepository;
        private readonly IRepository<MedicalHistory> medicalHistoryRepository;

        public PatientsController(IRepository<Patient> patientsRepository, IRepository<MedicalHistory> medicalHistoryRepository)
        {
            this.patientsRepository = patientsRepository;
            this.medicalHistoryRepository = medicalHistoryRepository;
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(patientsRepository.All().Select(p => new DisplayPatientDto(p.Id, p.FirstName, p.LastName, p.Mail, p.Age)));
        }

        [HttpPost]
        public IActionResult Create([FromBody] CreatePatientDto dto)
        {
            var patient = new Patient(dto.Email, dto.Password, dto.FirstName, dto.LastName, dto.Age);
            var medicalHistory = new MedicalHistory();

            patient.RegisterMedicalHistory(medicalHistory);

            medicalHistoryRepository.Add(medicalHistory);
            patientsRepository.Add(patient);

            medicalHistoryRepository.SaveChanges();
            patientsRepository.SaveChanges();
            return Ok(new { id = patient.Id });
        }
    }
}
