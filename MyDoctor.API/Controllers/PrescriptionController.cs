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
    public class PrescriptionController : ControllerBase
    {
        private readonly IRepository<Prescription> prescriptonRepository;
        public PrescriptionController(IRepository<Prescription> prescriptonRepository)
        {
            this.prescriptonRepository = prescriptonRepository;
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(prescriptonRepository.All());
        }
        [HttpPost]
        public IActionResult Create([FromBody] CreatePrescriptionDto dto)
        {
            var prescription = new Prescription(dto.Description, dto.Name);
            prescriptonRepository.Add(prescription);
            prescriptonRepository.SaveChanges();
            return Created(nameof(Get), prescription);
        }
    }
}
