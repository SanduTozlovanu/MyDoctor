using Microsoft.AspNetCore.Mvc;
using MyDoctor.API.DTOs;
using MyDoctorApp.Domain.Models;
using MyDoctorApp.Infrastructure.Generics;

namespace MyDoctor.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PrescriptedDrugController : ControllerBase
    {
        private readonly IRepository<PrescriptedDrug> prescriptedDrugRepository;

        public PrescriptedDrugController(IRepository<PrescriptedDrug> prescriptedDrugRepository)
        {
            this.prescriptedDrugRepository = prescriptedDrugRepository;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok((await prescriptedDrugRepository.AllAsync()).Select(pd => prescriptedDrugRepository.GetMapper().Map<DisplayPrescriptedDrugDto>(pd)));
        }
    }
}
