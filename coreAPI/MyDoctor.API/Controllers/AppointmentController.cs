﻿using Microsoft.AspNetCore.Mvc;
using MyDoctor.API.DTOs;
using MyDoctorApp.Domain.Models;
using MyDoctorApp.Infrastructure.Generics;

namespace MyDoctor.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AppointmentController : ControllerBase
    {
        public const string PatientNotFoundError = "Could not find a patient with this Id.";
        public const string DoctorNotFoundError = "Could not find a doctor with this Id.";
        private readonly IRepository<Appointment> appointmentRepository;
        private readonly IRepository<AppointmentInterval> appointmentIntervalRepository;
        private readonly IRepository<Bill> billRepository;
        private readonly IRepository<Doctor> doctorRepository;
        private readonly IRepository<Patient> patientsRepository;

        public AppointmentController(IRepository<Appointment> appointmentRepository,
            IRepository<Patient> patientsRepository,
            IRepository<Doctor> doctorRepository,
            IRepository<AppointmentInterval> appointmentIntervalRepository,
            IRepository<Bill> billRepository)
        {
            this.appointmentRepository = appointmentRepository;
            this.patientsRepository = patientsRepository;
            this.doctorRepository = doctorRepository;
            this.appointmentIntervalRepository = appointmentIntervalRepository;
            this.billRepository = billRepository;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok((await appointmentRepository.AllAsync()).Select(a => appointmentRepository.GetMapper().Map<DisplayAppointmentDto>(a)));
        }

        [HttpPost("{patientId:guid}_{doctorId:guid}/create_appointment")]
        public async Task<IActionResult> Create(Guid patientId, Guid doctorId, [FromBody] CreateAppointmentDto dto)
        {
            var patient = await patientsRepository.GetAsync(patientId);
            var doctor = await doctorRepository.GetAsync(doctorId);
            if (patient == null)
            {
                return NotFound(PatientNotFoundError);
            }
            if (doctor == null)
            {
                return NotFound(DoctorNotFoundError);
            }

            var appointment = new Appointment(patient, doctor, dto.Price);
            var appointmentInterval = new AppointmentInterval(
                appointment,
                DateOnly.FromDateTime(dto.Date),
                TimeOnly.FromDateTime(dto.StartTime),
                TimeOnly.FromDateTime(dto.EndTime)
                );
            var bill = new Bill(appointment);

            await billRepository.AddAsync(bill);
            await appointmentRepository.AddAsync(appointment);
            await appointmentIntervalRepository.AddAsync(appointmentInterval);

            await billRepository.SaveChangesAsync();
            await patientsRepository.SaveChangesAsync();
            await doctorRepository.SaveChangesAsync();
            await appointmentRepository.SaveChangesAsync();

            return Ok(appointmentRepository.GetMapper().Map<DisplayAppointmentDto>(appointment));
        }
    }


}
