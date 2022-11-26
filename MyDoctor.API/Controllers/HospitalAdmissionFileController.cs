using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyDoctor.API.Dtos;
using MyDoctor.API.DTOs;
using MyDoctor.Domain.Models;
using MyDoctorApp.Infrastructure.Generics;
using MyDoctorApp.Infrastructure.Generics.GenericRepositories;

namespace MyDoctor.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HospitalAdmissionFileController : ControllerBase
    {
        private readonly IRepository<HospitalAdmissionFile> hospitalAdmissioFileRepository;
        private readonly IRepository<Prescription> prescriptionRepository;
        private readonly IRepository<Hospital> hospitalRepository;

        public HospitalAdmissionFileController(IRepository<HospitalAdmissionFile> hospitalAdmissioFileRepository,
            IRepository<Prescription> prescriptionRepository,
            IRepository<Hospital> hospitalRepository)
        {
            this.hospitalAdmissioFileRepository = hospitalAdmissioFileRepository;
            this.prescriptionRepository = prescriptionRepository;
            this.hospitalRepository = hospitalRepository;
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(hospitalAdmissioFileRepository.All());
        }

        [HttpPost]
        public IActionResult Create(Guid prescriptionId, Guid hospitalId, [FromBody] CreateHospitalAdmissionFileDto dto)
        {
            var prescription = prescriptionRepository.Get(prescriptionId);
            var hospital = hospitalRepository.Get(hospitalId);

            if (prescription == null || hospital == null)
            {
                return NotFound();
            }

            HospitalAdmissionFile hospitalAdmissionFile = new HospitalAdmissionFile(dto.Name, dto.Description);

            hospitalAdmissionFile.AttachToHospital(hospital);
            prescription.RegisterHospitalAdmissionFile(hospitalAdmissionFile);

            hospitalAdmissioFileRepository.Add(hospitalAdmissionFile);
            hospitalAdmissioFileRepository.SaveChanges();
            prescriptionRepository.SaveChanges();

            return Ok();
        }
    }
}
