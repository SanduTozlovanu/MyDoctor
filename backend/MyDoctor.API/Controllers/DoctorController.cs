using Microsoft.AspNetCore.Mvc;
using MyDoctor.API.DTOs;
using MyDoctor.Domain.Models;
using MyDoctorApp.Infrastructure.Generics;

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
            return Ok(doctorRepository.All().Select(d => new DisplayDoctorDto(d.Id, d.MedicalRoomId, d.Email, d.Speciality, d.FirstName, d.LastName)));
        }
        [HttpPost]
        public IActionResult Create([FromBody] CreateDoctorDto dto)
        {
            List<MedicalRoom> medicalRooms = medicalRoomRepository.All().ToList();
            (MedicalRoom?, int) medicalRoomWithFewestDoctors = new(null, int.MaxValue);
            medicalRooms.ForEach(mr =>
            {
                int doctorNumber = doctorRepository.Find(d => mr.Id == d.MedicalRoomId).Count();
                if(doctorNumber < medicalRoomWithFewestDoctors.Item2)
                    medicalRoomWithFewestDoctors = (mr, doctorNumber);

            });
            if (medicalRoomWithFewestDoctors.Item1 == null)
            {
                return NotFound("Could not find a free medical room for this doctor.");
            }
            MedicalRoom medicalRoom = medicalRoomWithFewestDoctors.Item1;
            if (medicalRoom == null)
            {
                return NotFound("Could not find a medicalRoom with this Id.");
            }

            var doctor = new Doctor(dto.FirstName, dto.LastName, dto.Speciality, dto.Email, dto.Password);
            medicalRoom.RegisterDoctors(new List<Doctor> { doctor });

            doctorRepository.Add(doctor);
            doctorRepository.SaveChanges();
            medicalRoomRepository.SaveChanges();

            return Ok(new DisplayDoctorDto(doctor.Id, doctor.MedicalRoomId, doctor.Email, doctor.Speciality, doctor.FirstName, doctor.LastName));
        }
    }
}
