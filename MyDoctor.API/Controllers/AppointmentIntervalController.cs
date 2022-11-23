using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyDoctor.Domain.Models;
using MyDoctorApp.Infrastructure.Generics;

namespace MyDoctor.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AppointmentIntervalController : ControllerBase
    {
        private readonly IRepository<AppointmentInterval> appointmentIntervalRepository;
        private readonly IRepository<Appointment> appointmentRepository;
        private readonly IRepository<Doctor> doctorRepository;
    }
}
