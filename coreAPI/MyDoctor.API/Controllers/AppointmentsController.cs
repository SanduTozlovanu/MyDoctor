using Microsoft.AspNetCore.Mvc;
using MyDoctor.API.DTOs;
using MyDoctorApp.Domain.Models;
using MyDoctorApp.Infrastructure.Generics;

namespace MyDoctor.API.Controllers
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("1.0")]
    public class AppointmentsController : ControllerBase
    {
        public const string PatientNotFoundError = "Could not find a patient with this Id.";
        public const string DoctorNotFoundError = "Could not find a doctor with this Id.";
        private readonly IRepository<Appointment> appointmentRepository;
        private readonly IRepository<AppointmentInterval> appointmentIntervalRepository;
        private readonly IRepository<Bill> billRepository;
        private readonly IRepository<Doctor> doctorRepository;
        private readonly IRepository<Patient> patientsRepository;

        public AppointmentsController(IRepository<Appointment> appointmentRepository,
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

            var appointment = new Appointment(dto.Price);

            patient.RegisterAppointment(appointment);
            doctor.RegisterAppointment(appointment);
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
