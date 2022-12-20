using Microsoft.AspNetCore.Mvc;
using MyDoctor.API.DTOs;
using MyDoctorApp.Domain.Models;
using MyDoctorApp.Infrastructure.Generics;

namespace MyDoctor.API.Controllers
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("1.0")]
    public class BillsController : ControllerBase
    {
        private readonly IRepository<Bill> billRepository;

        public BillsController(IRepository<Bill> billRepository)
        {
            this.billRepository = billRepository;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok((await billRepository.AllAsync()).Select(b => billRepository.GetMapper().Map<DisplayBillDto>(b)));
        }
    }


}
