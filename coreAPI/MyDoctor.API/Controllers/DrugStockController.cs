using Microsoft.AspNetCore.Mvc;
using MyDoctor.API.DTOs;
using MyDoctorApp.Domain.Models;
using MyDoctorApp.Infrastructure.Generics;

namespace MyDoctor.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DrugStockController : ControllerBase
    {
        private readonly IRepository<DrugStock> drugStockRepository;

        public DrugStockController(IRepository<DrugStock> drugStockRepository)
        {
            this.drugStockRepository = drugStockRepository;
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(drugStockRepository.All().Select(d => drugStockRepository.GetMapper().Map<DisplayDrugStockDto>(d)));
        }
    }
}
