using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyDoctor.Domain.Models;
using MyDoctorApp.Infrastructure.Generics;

namespace MyDoctor.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BillController : ControllerBase
    {
        private readonly IRepository<Bill> billRepository;
        private readonly IRepository<Appointment> appointmentRepository;
    }
}
