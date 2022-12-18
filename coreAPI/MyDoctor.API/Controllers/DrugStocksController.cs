using Microsoft.AspNetCore.Mvc;
using MyDoctor.API.DTOs;
using MyDoctorApp.Domain.Models;
using MyDoctorApp.Infrastructure.Generics;

namespace MyDoctor.API.Controllers
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("1.0")]
    public class DrugStocksController : ControllerBase
    {
        private readonly IRepository<DrugStock> drugStockRepository;

        public DrugStocksController(IRepository<DrugStock> drugStockRepository)
        {
            this.drugStockRepository = drugStockRepository;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok((await drugStockRepository.AllAsync()).Select(d => drugStockRepository.GetMapper().Map<DisplayDrugStockDto>(d)));
        }
    }
}
