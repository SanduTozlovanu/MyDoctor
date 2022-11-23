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
    public class DoctorController : ControllerBase
    {
        private readonly IRepository<Doctor> doctorRepository;
        private readonly IRepository<MedicalRoom> medicalRoomRepository;

        public DoctorController(IRepository<Doctor> doctorRepository, IRepository<MedicalRoom> medicalRoomRepository)
        {
            this.doctorRepository = doctorRepository;
            this.medicalRoomRepository = medicalRoomRepository;
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(doctorRepository.All());
        }
        [HttpPost]
        public IActionResult Create([FromBody] CreateDoctorDto dto)
        {
            var doctor = new Doctor(dto.FirstName, dto.LastName, dto.Speciality, dto.Mail, dto.Password);
            doctorRepository.Add(doctor);
            doctorRepository.SaveChanges();
            return Created(nameof(Get), doctor);
        }
    }
}
