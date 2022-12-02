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

        public AppointmentIntervalController(IRepository<AppointmentInterval> appointmentIntervalRepository)
        {
            this.appointmentIntervalRepository = appointmentIntervalRepository;
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(appointmentIntervalRepository.All());
        }
    }
}
