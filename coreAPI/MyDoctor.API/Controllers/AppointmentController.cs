using Microsoft.AspNetCore.Mvc;
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
        public IActionResult Get()
        {
            return Ok(appointmentRepository.All().Select(a => appointmentRepository.GetMapper().Map<DisplayAppointmentDto>(a)));
        }

        [HttpPost("{patientId:guid}_{doctorId:guid}/create_appointment")]
        public IActionResult Create(Guid patientId, Guid doctorId, [FromBody] CreateAppointmentDto dto)
        {
            var patient = patientsRepository.Get(patientId);
            var doctor = doctorRepository.Get(doctorId);
            if (patient == null)
            {
                return NotFound(PatientNotFoundError);
            }
            if (doctor == null)
            {
                return NotFound(DoctorNotFoundError);
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

            return Ok(appointmentRepository.GetMapper().Map<DisplayAppointmentDto>(appointment));
        }
    }


}
