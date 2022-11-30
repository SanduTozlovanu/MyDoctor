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
            return Ok(doctorRepository.All().Select(d => new DisplayDoctorDto(d.Id, d.MedicalRoomId, d.Mail, d.Speciality, d.FirstName, d.LastName)));
        }
        [HttpPost]
        public IActionResult Create(Guid medicalRoomId, [FromBody] CreateDoctorDto dto)
        {
            var medicalRoom = medicalRoomRepository.Get(medicalRoomId);
            if (medicalRoom == null)
            {
                return NotFound("Could not find a medicalRoom with this Id.");
            }

            var doctor = new Doctor(dto.FirstName, dto.LastName, dto.Speciality, dto.Mail, dto.Password);
            medicalRoom.RegisterDoctors(new List<Doctor> { doctor });

            doctorRepository.Add(doctor);
            doctorRepository.SaveChanges();
            medicalRoomRepository.SaveChanges();

            return Ok(new { id = doctor.Id });
        }
    }
}
