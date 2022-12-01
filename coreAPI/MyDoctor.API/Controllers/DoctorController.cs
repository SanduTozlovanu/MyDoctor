﻿using Microsoft.AspNetCore.Mvc;
using MyDoctor.API.DTOs;
using MyDoctor.API.Helpers;
using MyDoctor.Domain.Models;
using MyDoctorApp.Infrastructure.Generics;

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
            return Ok(doctorsRepository.All().Select(d => new DisplayDoctorDto(d.Id, d.MedicalRoomId, d.Email, d.Speciality, d.FirstName, d.LastName)));
        }
        [HttpPost]
        public IActionResult Create([FromBody] CreateDoctorDto dto)
        {
            List<MedicalRoom> medicalRooms = medicalRoomRepository.All().ToList();
            (MedicalRoom?, int) medicalRoomWithFewestDoctors = new(null, int.MaxValue);
            medicalRooms.ForEach(mr =>
            {
                int doctorNumber = doctorsRepository.Find(d => mr.Id == d.MedicalRoomId).Count();
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
            var doctor = new Doctor(dto.UserDetails.Email, hashedPassword, dto.UserDetails.FirstName, dto.UserDetails.LastName, dto.Speciality);
            medicalRoom.RegisterDoctors(new List<Doctor> { doctor });

            doctorsRepository.Add(doctor);
            doctorsRepository.SaveChanges();
            medicalRoomRepository.SaveChanges();

            return Ok(new DisplayDoctorDto(doctor.Id, doctor.MedicalRoomId, doctor.Email, doctor.Speciality, doctor.FirstName, doctor.LastName));
        }
    }
}