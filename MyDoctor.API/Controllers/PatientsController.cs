using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyDoctor.API.DTOs;
using MyDoctor.Domain.Models;
using MyDoctorApp.Infrastructure.Generics;
using MyDoctorApp.Infrastructure.Generics.GenericRepositories;

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

        [HttpPost("{patientId:guid}/{medicalHistoryId:guid}")]
        public IActionResult RegisterMedicalHistory(Guid patientId, Guid medicalHistoryId)
        {
            var patient = patientsRepository.Get(patientId);
            var medicalHistory = medicalHistoryRepository.Get(medicalHistoryId);

            if (patient == null || medicalHistory == null)
            {
                return NotFound();
            }

            patient.RegisterMedicalHistory(medicalHistory);


            medicalHistoryRepository.SaveChanges();
            patientsRepository.SaveChanges();

            return NoContent();
        }

        [HttpPost]
        public IActionResult Create([FromBody] CreatePatientDto dto)
        {
            var patient = new Patient(dto.Email, dto.Password, dto.FirstName, dto.LastName, dto.Age);
            patientsRepository.Add(patient);
            patientsRepository.SaveChanges();
            return Created(nameof(Get), patient);
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(patientsRepository.All());
        }
    }
}
