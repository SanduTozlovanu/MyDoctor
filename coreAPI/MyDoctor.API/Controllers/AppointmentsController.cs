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
        public const string TimeFormatInvalid = "Could not convert time string to TimeOnly";
        private const string AppointmentIntervalNotFoundError = "Could not find the AppointmentInterval of an appointment.";
        private readonly IRepository<Appointment> appointmentRepository;
        private readonly IRepository<AppointmentInterval> appointmentIntervalRepository;
        private readonly IRepository<Bill> billRepository;
        private readonly IRepository<Doctor> doctorRepository;
        private readonly IRepository<Patient> patientRepository;

        public AppointmentsController(IRepository<Appointment> appointmentRepository,
            IRepository<Patient> patientRepository,
            IRepository<Doctor> doctorRepository,
            IRepository<AppointmentInterval> appointmentIntervalRepository,
            IRepository<Bill> billRepository)
        {
            this.appointmentRepository = appointmentRepository;
            this.patientRepository = patientRepository;
            this.doctorRepository = doctorRepository;
            this.appointmentIntervalRepository = appointmentIntervalRepository;
            this.billRepository = billRepository;
        }

        [HttpGet("doctors/{doctorId:guid}")]
        public async Task<IActionResult> GetPatientsInformation(Guid doctorId)
        {
            var doctor = await doctorRepository.GetAsync(doctorId);
            if (doctor == null)
            {
                return BadRequest(DoctorNotFoundError);
            }
            var appointmentList = (await appointmentRepository.FindAsync(app => app.DoctorId == doctorId)).ToList();
            List<DisplayAppointmentPatientInformationDto> appointmentInformationList = new();
            IActionResult? error = null;
            appointmentList.ForEach(async app =>
            {
                var appointmentInterval = (await appointmentIntervalRepository.FindAsync(appInt => appInt.AppointmentId == app.Id)).FirstOrDefault();
                if (appointmentInterval == null)
                {
                    error = NotFound(AppointmentIntervalNotFoundError);
                    return;
                }
                var patient = (await patientRepository.FindAsync(p => p.Id == app.PatientId)).FirstOrDefault();
                if (patient == null)
                {
                    error = NotFound(PatientNotFoundError);
                    return;
                }
                appointmentInformationList.Add(new DisplayAppointmentPatientInformationDto(appointmentInterval.Date.ToString("yyyy-MM-dd"),
                    patient.FirstName, patient.LastName, patient.Email, appointmentInterval.StartTime.ToString("HH:mm"),
                    appointmentInterval.EndTime.ToString("HH:mm")));
            });
            return error ?? Ok(appointmentInformationList);
        }
        [HttpGet("patients/{patientId:guid}")]
        public async Task<IActionResult> GetDoctorInformation(Guid patientId)
        {
            var patient = await patientRepository.GetAsync(patientId);
            if (patient == null)
            {
                return BadRequest(PatientNotFoundError);
            }
            var appointmentList = (await appointmentRepository.FindAsync(app => app.PatientId == patientId)).ToList();
            List<DisplayAppointmentDoctorInformationDto> appointmentInformationList = new();
            IActionResult? error = null;
            appointmentList.ForEach(async app =>
            {
                var appointmentInterval = (await appointmentIntervalRepository.FindAsync(appInt => appInt.AppointmentId == app.Id)).FirstOrDefault();
                if (appointmentInterval == null)
                {
                    error = NotFound(AppointmentIntervalNotFoundError);
                    return;
                }
                var doctor = (await doctorRepository.FindAsync(p => p.Id == app.DoctorId)).FirstOrDefault();
                if (doctor == null)
                {
                    error = NotFound(DoctorNotFoundError);
                    return;
                }
                appointmentInformationList.Add(new DisplayAppointmentDoctorInformationDto(appointmentInterval.Date.ToString("yyyy-MM-dd"),
                    doctor.FirstName, doctor.LastName, doctor.Email, appointmentInterval.StartTime.ToString("HH:mm"),
                    appointmentInterval.EndTime.ToString("HH:mm")));
            });
            return error ?? Ok(appointmentInformationList);
        }
        /// <summary>
        /// Endpoint for creating an appointment
        /// </summary>
        /// <remarks>
        /// Parameters remarks
        /// 
        ///     date field format is: "yyyy-mm-d"
        ///         example: "date" : "2023-12-24"
        ///         
        ///     startTime format is: "hh:mm"
        ///         example: "startTime" : "06:00"
        ///         
        ///     endTime format is: "hh:mm"
        ///         example: "endTime" : "07:00"
        /// </remarks>
        [HttpPost("{patientId:guid}_{doctorId:guid}/create_appointment")]
        public async Task<IActionResult> Create(Guid patientId, Guid doctorId, [FromBody] CreateAppointmentDto dto)
        {
            var patient = await patientRepository.GetAsync(patientId);
            var doctor = await doctorRepository.GetAsync(doctorId);
            if (patient == null)
            {
                return NotFound(PatientNotFoundError);
            }
            if (doctor == null)
            {
                return NotFound(DoctorNotFoundError);
            }
            TimeOnly startTime;
            TimeOnly endTime;
            try
            {
                startTime = TimeOnly.Parse(dto.StartTime);
                endTime = TimeOnly.Parse(dto.EndTime);

            }
            catch (Exception ex) when (ex is ArgumentNullException ||
                           ex is FormatException)
            {
                return BadRequest(TimeFormatInvalid);
            }


            var appointment = new Appointment();

            patient.RegisterAppointment(appointment);
            doctor.RegisterAppointment(appointment);
            var appointmentInterval = new AppointmentInterval(
                dto.Date,
                startTime,
                endTime
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
            await patientRepository.SaveChangesAsync();
            await doctorRepository.SaveChangesAsync();
            await appointmentRepository.SaveChangesAsync();

            return Ok(appointmentRepository.GetMapper().Map<DisplayAppointmentDto>(appointment));
        }
    }
}
