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
    public class AppointmentController : ControllerBase
    {
        private readonly IRepository<Appointment> appointmentRepository;
        private readonly IRepository<AppointmentInterval> appointmentIntervalRepository;
        private readonly IRepository<Bill> billRepository;
        private readonly IRepository<Doctor> doctorRepository;
        private readonly IRepository<Patient> patientsRepository;
        private readonly IRepository<Prescription> prescriptonRepository;

        public AppointmentController(IRepository<Appointment> appointmentRepository,
            IRepository<Patient> patientsRepository,
            IRepository<Doctor> doctorRepository,
            IRepository<AppointmentInterval> appointmentIntervalRepository)
        {
            this.appointmentRepository = appointmentRepository;
            this.patientsRepository = patientsRepository;
            this.doctorRepository = doctorRepository;
            this.appointmentIntervalRepository = appointmentIntervalRepository;
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(appointmentRepository.All());
        }

        [HttpPost]
        public IActionResult Create(Guid patientId, Guid doctorId, [FromBody] CreateAppointmentDto dto)
        {
            var patient = patientsRepository.Get(patientId);
            var doctor = doctorRepository.Get(doctorId);
            if (patient == null || doctor == null)
            {
                return NotFound();
            }

            var appointment = new Appointment(dto.Price);
            var appointmentInterval = new AppointmentInterval(
                DateOnly.FromDateTime(dto.Date),
                TimeOnly.FromDateTime(dto.StartTime),
                TimeOnly.FromDateTime(dto.EndTime)
                );

            appointment.AttachToPatient(patient);
            appointment.AttachToDoctor(doctor);
            appointment.RegisterAppointmentInterval(appointmentInterval);

            appointmentRepository.Add(appointment);
            appointmentIntervalRepository.Add(appointmentInterval);

            patientsRepository.SaveChanges();
            doctorRepository.SaveChanges();
            appointmentRepository.SaveChanges();

            return NoContent();
        }
    }


}
