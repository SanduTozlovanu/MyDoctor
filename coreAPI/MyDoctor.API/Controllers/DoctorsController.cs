﻿using Microsoft.AspNetCore.Mvc;
using MyDoctor.API.DTOs;
using MyDoctor.API.Helpers;
using MyDoctor.Application.Mappers.MedicalRoomMappers;
using MyDoctor.Application.Mappers.ScheduleIntervalMappers;
using MyDoctor.Application.Response;
using MyDoctorApp.Domain.Helpers;
using MyDoctorApp.Domain.Models;
using MyDoctorApp.Infrastructure.Generics;

namespace MyDoctor.API.Controllers
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("1.0")]
    public class DoctorsController : ControllerBase
    {
        public const string SpecialityNotFoundError = "Could not find specialty by the specialityId provided.";
        public const string FreeMedicalRoomNotFoundError = "Could not find a free medical room for this doctor.";
        public const string MedicalRoomNotFoundError = "Could not find a medicalRoom with this Id.";
        public const string UsedEmailError = "The email is already used!";
        public const string InvalidEmailError = "The email is invalid!";
        public const string CouldNotCreateDoctorError = "Could not create a doctor from the dto.";
        private const string InvalidDoctorIdError = "There is no such Doctor with this id.";
        private readonly IRepository<Doctor> doctorRepository;
        private readonly IRepository<MedicalRoom> medicalRoomRepository;
        private readonly IRepository<Patient> patientRepository;
        private readonly IRepository<Speciality> specialityRepository;
        private readonly IRepository<ScheduleInterval> scheduleIntervalRepository;
        private readonly IRepository<Appointment> appointmentsRepository;
        private readonly IRepository<AppointmentInterval> appointmentIntervalsRepository;

        public DoctorsController(IRepository<Doctor> doctorRepository,
            IRepository<MedicalRoom> medicalRoomRepository,
            IRepository<Patient> patientRepository,
            IRepository<Speciality> specialityRepository,
            IRepository<ScheduleInterval> scheduleIntervalRepository,
            IRepository<Appointment> appointmentsRepository,
            IRepository<AppointmentInterval> appointmentIntervalsRepository)
        {
            this.doctorRepository = doctorRepository;
            this.medicalRoomRepository = medicalRoomRepository;
            this.patientRepository = patientRepository;
            this.specialityRepository = specialityRepository;
            this.scheduleIntervalRepository = scheduleIntervalRepository;
            this.appointmentsRepository = appointmentsRepository;
            this.appointmentIntervalsRepository = appointmentIntervalsRepository;
        }

        private static List<ScheduleInterval> GenerateScheduleIntervals()
        {
            var scheduleIntervals = new List<ScheduleInterval>();
            foreach (var day in Enum.GetNames(typeof(WeekDays)))
            {
                scheduleIntervals.Add(new ScheduleInterval(day, new TimeOnly(0, 0), new TimeOnly(23, 59)));
            }
            return scheduleIntervals;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok((await doctorRepository.AllAsync()).Select(d => doctorRepository.GetMapper().Map<DisplayDoctorDto>(d)));
        }

        [HttpGet("get_by_speciality/{specialityId:guid}")]
        public async Task<IActionResult> GetBySpeciality(Guid specialityId)
        {
            Speciality? speciality = await specialityRepository.GetAsync(specialityId);
            if (speciality == null)
            {
                return NotFound(SpecialityNotFoundError);
            }

            var doctors = await doctorRepository.FindAsync(d => d.SpecialityID == specialityId);

            return Ok(doctors.Select(d => doctorRepository.GetMapper().Map<DisplayDoctorDto>(d)));
        }

        [HttpGet("get_available_appointment_schedule/{doctorId:guid}")]
        public async Task<IActionResult> GetAvailableAppointmentSchedule(Guid doctorId, DateOnly dateOnly)
        {

            var doctor = await doctorRepository.GetAsync(doctorId);
            if (doctor == null)
            {
                return NotFound();
            }

            var scheduleIntervs = (await scheduleIntervalRepository.FindAsync(si => si.DoctorId == doctorId)).ToList();
            var appointments = (await appointmentsRepository.FindAsync(a => a.DoctorId == doctorId)).ToList();
            var appointmentsIntervs = new List<AppointmentInterval>();
            foreach (var appointment in appointments)
            {
                var appointmentInterval = (await appointmentIntervalsRepository.FindAsync(ai => ai.AppointmentId == appointment.Id)).SingleOrDefault();
                if (appointmentInterval != null)
                {
                    appointmentsIntervs.Add(appointmentInterval);
                }
            }
            var intervs = new List<AvailableAppointmentIntervalsResponse>();
            foreach (var interval in doctor.GetAvailableAppointmentIntervals(dateOnly, scheduleIntervs, appointmentsIntervs))
            {
                intervs.Add(new AvailableAppointmentIntervalsResponse(interval.Item1, interval.Item2));
            }

            return Ok(intervs);
        }

        [HttpPost("speciality")]
        public async Task<IActionResult> Create([FromBody] CreateDoctorDto dto)
        {
            var specialityId = dto.SpecialityId;
            Speciality? speciality = await specialityRepository.GetAsync(specialityId);
            if (speciality == null)
            {
                return NotFound(SpecialityNotFoundError);
            }

            List<MedicalRoom> medicalRooms = (await medicalRoomRepository.AllAsync()).ToList();
            (MedicalRoom?, int) medicalRoomWithFewestDoctors = new(null, int.MaxValue);
            medicalRooms.ForEach(async mr =>
            {
                int doctorNumber = (await doctorRepository.FindAsync(d => mr.Id == d.MedicalRoomId)).Count();
                if (doctorNumber < medicalRoomWithFewestDoctors.Item2)
                    medicalRoomWithFewestDoctors = (mr, doctorNumber);

            });
            if (medicalRoomWithFewestDoctors.Item1 == null)
            {
                return NotFound(FreeMedicalRoomNotFoundError);
            }
            MedicalRoom medicalRoom = medicalRoomWithFewestDoctors.Item1;
            if (medicalRoom == null)
            {
                return NotFound(MedicalRoomNotFoundError);
            }


            var scheduleIntervals = GenerateScheduleIntervals();
            foreach (var scheduleInterval in scheduleIntervals)
            {
                await scheduleIntervalRepository.AddAsync(scheduleInterval);
            }

            var ActionResultDoctorTuple = await CreateDoctorFromDto(dto);

            if (ActionResultDoctorTuple.Item2.GetType() != typeof(OkResult))
                return ActionResultDoctorTuple.Item2;

            if (ActionResultDoctorTuple.Item1 == null)
                return BadRequest(ActionResultDoctorTuple);

            Doctor doctor = ActionResultDoctorTuple.Item1;
            medicalRoom.RegisterDoctors(new List<Doctor> { doctor });
            doctor.RegisterScheduleIntervals(scheduleIntervals);
            speciality.RegisterDoctor(doctor);

            await doctorRepository.AddAsync(doctor);
            await doctorRepository.SaveChangesAsync();
            await medicalRoomRepository.SaveChangesAsync();
            await specialityRepository.SaveChangesAsync();
            await scheduleIntervalRepository.SaveChangesAsync();

            return Ok(doctorRepository.GetMapper().Map<DisplayDoctorDto>(doctor));
        }



        [HttpPut("{doctorId:guid}")]
        public async Task<IActionResult> Update(Guid doctorId, [FromBody] CreateDoctorDto dto)
        {
            var doctor = await doctorRepository.GetAsync(doctorId);
            if (doctor == null)
            {
                return NotFound();
            }

            var ActionResultDoctorTuple = await CreateDoctorFromDto(dto);

            if (ActionResultDoctorTuple.Item2.GetType() != typeof(OkResult))
                return ActionResultDoctorTuple.Item2;

            if (ActionResultDoctorTuple.Item1 == null)
                return BadRequest(ActionResultDoctorTuple);

            doctor.Update(ActionResultDoctorTuple.Item1);

            doctorRepository.Update(doctor);

            await doctorRepository.SaveChangesAsync();
            return Ok();
        }


        [HttpDelete("{doctorId:guid}")]
        public async Task<IActionResult> Delete(Guid doctorId)
        {
            try
            {
                await doctorRepository.Delete(doctorId);
            }catch(ArgumentException) 
            {
                return BadRequest(InvalidDoctorIdError);
            }

            await doctorRepository.SaveChangesAsync();
            return Ok();
        }

        private async Task<(Doctor?, IActionResult)> CreateDoctorFromDto(CreateDoctorDto dto)
        {
            var oldPatient = (await patientRepository.FindAsync(p => p.Email == dto.UserDetails.Email)).FirstOrDefault();
            var oldDoctor = (await doctorRepository.FindAsync(d => d.Email == dto.UserDetails.Email)).FirstOrDefault();
            if (oldPatient != null || oldDoctor != null)
            {
                return (null, BadRequest(UsedEmailError));
            }

            if (!AccountInfoManager.ValidateEmail(dto.UserDetails.Email))
            {
                return (null, BadRequest(InvalidEmailError));
            }

            string hashedPassword = AccountInfoManager.HashPassword(dto.UserDetails.Password);
            var newDoctor = new Doctor(dto.UserDetails.Email, hashedPassword, dto.UserDetails.FirstName, dto.UserDetails.LastName);

            return (newDoctor, Ok());
        }
    }
}