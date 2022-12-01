using Microsoft.AspNetCore.Mvc;
using MyDoctor.API.DTOs;
using MyDoctor.Domain.Models;
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
        public IActionResult Get()
        {
            return Ok(prescriptedDrugRepository.All().Select(pd => new DisplayPrescriptedDrugDto(pd.Id, pd.DrugId, pd.PrescriptionId, pd.Quantity)));
        }
    }
}
