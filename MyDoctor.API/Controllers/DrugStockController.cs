using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyDoctor.API.Dtos;
using MyDoctor.Domain.Models;
using MyDoctorApp.Infrastructure.Generics;
using MyDoctorApp.Infrastructure.Generics.GenericRepositories;

namespace MyDoctor.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DrugStockController : ControllerBase
    {
        private readonly IRepository<DrugStock> drugStockRepository;
        private readonly IRepository<Drug> drugRepository;

        public DrugStockController(IRepository<DrugStock> drugStockRepository)
        {
            this.drugStockRepository = drugStockRepository;
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(drugStockRepository.All());
        }
    }
}
