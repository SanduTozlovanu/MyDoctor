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
        public IActionResult Get()
        {
            return Ok(appointmentRepository.All().Select(a => new DisplayAppointmentDto(a.Id, a.PatientId, a.DoctorId, a.Price)));
        }

        [HttpPost("{patientId:guid}_{doctorId:guid}/create_appointment")]
        public IActionResult Create(Guid patientId, Guid doctorId, [FromBody] CreateAppointmentDto dto)
        {
            var patient = patientsRepository.Get(patientId);
            var doctor = doctorRepository.Get(doctorId);
            if (patient == null)
            {
                return NotFound("Could not find a patient with this Id.");
            }
            if (doctor == null)
            {
                return NotFound("Could not find a doctor with this Id.");
            }

            var appointment = new Appointment(dto.Price);
            var appointmentInterval = new AppointmentInterval(
                DateOnly.FromDateTime(dto.Date),
                TimeOnly.FromDateTime(dto.StartTime),
                TimeOnly.FromDateTime(dto.EndTime)
                );
            var bill = new Bill();

            patient.RegisterAppointment(appointment);
            doctor.RegisterAppointment(appointment);
            appointment.RegisterAppointmentInterval(appointmentInterval);
            appointment.RegisterBill(bill);

            billRepository.Add(bill);
            appointmentRepository.Add(appointment);
            appointmentIntervalRepository.Add(appointmentInterval);

            billRepository.SaveChanges();
            patientsRepository.SaveChanges();
            doctorRepository.SaveChanges();
            appointmentRepository.SaveChanges();

            return Ok(new { id = appointment.Id });
        }
    }


}
