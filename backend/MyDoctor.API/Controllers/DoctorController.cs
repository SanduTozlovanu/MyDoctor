using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyDoctor.API.Dtos;
using MyDoctor.API.DTOs;
using MyDoctor.API.Helpers;
using MyDoctor.Domain.Models;
using MyDoctorApp.Infrastructure.Generics;
using MyDoctorApp.Infrastructure.Generics.GenericRepositories;

namespace MyDoctor.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DoctorController : ControllerBase
    {
        private readonly IRepository<Doctor> doctorsRepository;
        private readonly IRepository<MedicalRoom> medicalRoomRepository;
        private readonly IRepository<Patient> patientsRepository;

        public DoctorController(IRepository<Doctor> doctorsRepository,
            IRepository<MedicalRoom> medicalRoomRepository,
            IRepository<Patient> patientsRepository)
        {
            this.doctorsRepository = doctorsRepository;
            this.medicalRoomRepository = medicalRoomRepository;
            this.patientsRepository = patientsRepository;
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(doctorsRepository.All().Select(d => new DisplayDoctorDto(d.Id, d.MedicalRoomId, d.Mail, d.Speciality, d.FirstName, d.LastName)));
        }
        [HttpPost]
        public IActionResult Create(Guid medicalRoomId, [FromBody] CreateDoctorDto dto)
        {
            var medicalRoom = medicalRoomRepository.Get(medicalRoomId);
            if (medicalRoom == null)
            {
                return NotFound("Could not find a medicalRoom with this Id.");
            }

            var oldPatient = patientsRepository.Find(p => p.Mail == dto.UserDetails.Email).FirstOrDefault();
            var oldDoctor = doctorsRepository.Find(d => d.Mail == dto.UserDetails.Email).FirstOrDefault();
            if (oldPatient != null || oldDoctor != null)
            {
                return BadRequest("The email is already used!");
            }

            if (!AccountInfoManager.ValidateEmail(dto.UserDetails.Email))
            {
                return BadRequest("The email is invalid!");
            }

            var doctor = new Doctor(dto.UserDetails.FirstName, dto.UserDetails.LastName, dto.Speciality, dto.UserDetails.Email, dto.UserDetails.Password);
            medicalRoom.RegisterDoctors(new List<Doctor> { doctor });

            doctorsRepository.Add(doctor);
            doctorsRepository.SaveChanges();
            medicalRoomRepository.SaveChanges();

            return Ok(new { id = doctor.Id });
        }
    }
}
