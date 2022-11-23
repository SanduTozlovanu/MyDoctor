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
    public class DrugController : ControllerBase
    {
        private readonly IRepository<Drug> drugRepository;

        public DrugController(IRepository<Drug> drugRepository)
        {
            this.drugRepository = drugRepository;
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(drugRepository.All());
        }

        [HttpPost]
        public IActionResult Create([FromBody] CreateDrugDto dto)
        {
            var drug = new Drug(dto.Name, dto.Description, dto.Price, dto.Quantity);
            drugRepository.Add(drug);
            drugRepository.SaveChanges();
            return Created(nameof(Get), drug);
        }
    }
}
