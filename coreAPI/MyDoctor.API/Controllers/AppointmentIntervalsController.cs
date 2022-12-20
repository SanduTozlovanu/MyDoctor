using Microsoft.AspNetCore.Mvc;
using MyDoctorApp.Domain.Models;
using MyDoctorApp.Infrastructure.Generics;

namespace MyDoctor.API.Controllers
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("1.0")]
    public class AppointmentIntervalsController : ControllerBase
    {
        private readonly IRepository<AppointmentInterval> appointmentIntervalRepository;

        public AppointmentIntervalsController(IRepository<AppointmentInterval> appointmentIntervalRepository)
        {
            this.appointmentIntervalRepository = appointmentIntervalRepository;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok(await appointmentIntervalRepository.AllAsync());
        }
    }
}
