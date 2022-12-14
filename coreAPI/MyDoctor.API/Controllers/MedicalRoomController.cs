using Microsoft.AspNetCore.Mvc;
using MyDoctor.API.DTOs;
using MyDoctorApp.Domain.Models;
using MyDoctorApp.Infrastructure.Generics;

namespace MyDoctor.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MedicalRoomController : ControllerBase
    {
        private readonly IRepository<MedicalRoom> medicalRoomRepository;
        private readonly IRepository<DrugStock> drugStockRepository;

        public MedicalRoomController(IRepository<MedicalRoom> medicalRoomRepository,
            IRepository<DrugStock> drugStockRepository)
        {
            this.medicalRoomRepository = medicalRoomRepository;
            this.drugStockRepository = drugStockRepository;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok((await medicalRoomRepository.AllAsync()).Select(mr => medicalRoomRepository.GetMapper().Map<DisplayMedicalRoomDto>(mr)));
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateMedicalRoomDto dto)
        {
            var medicalRoom = new MedicalRoom(dto.Adress);
            var drugStock = new DrugStock(medicalRoom);

            await medicalRoomRepository.AddAsync(medicalRoom);
            await drugStockRepository.AddAsync(drugStock);

            await medicalRoomRepository.SaveChangesAsync();
            await drugStockRepository.SaveChangesAsync();

            return Ok(medicalRoomRepository.GetMapper().Map<DisplayMedicalRoomDto>(medicalRoom));
        }
    }
}
