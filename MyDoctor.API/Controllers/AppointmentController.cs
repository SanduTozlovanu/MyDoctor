using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyDoctor.Domain.Models;
using MyDoctorApp.Infrastructure.Generics;

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
    }
}
