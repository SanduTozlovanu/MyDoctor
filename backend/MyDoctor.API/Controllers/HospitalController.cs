using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyDoctor.API.Dtos;
using MyDoctor.API.DTOs;
using MyDoctor.Domain.Models;
using MyDoctorApp.Infrastructure.Generics;
using MyDoctorApp.Infrastructure.Generics.GenericRepositories;

namespace MyDoctor.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HospitalController : ControllerBase
    {
        private readonly IRepository<Hospital> hospitalRepository;

        public HospitalController(IRepository<Hospital> hospitalRepository)
        {
            this.hospitalRepository = hospitalRepository;
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(hospitalRepository.All());
        }
        [HttpPost]
        public IActionResult Create([FromBody] CreateHospitalDto dto)
        {
            var hospital = new Hospital(dto.Name, dto.Adress);
            hospitalRepository.Add(hospital);
            hospitalRepository.SaveChanges();
            return Created(nameof(Get), new { id = hospital.Id });
        }
    }
}
