using Microsoft.AspNetCore.Mvc;
using MyDoctor.API.Dtos;
using MyDoctor.Domain.Models;
using MyDoctorApp.Infrastructure.Generics;

namespace MyDoctor.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MedicalRoomController : ControllerBase
    {
        private readonly IRepository<MedicalRoom> medicalRoomRepository;
        private readonly IRepository<DrugStock> drugStockRepository;
        private readonly IRepository<Doctor> doctorRepository;

        public MedicalRoomController(IRepository<MedicalRoom> medicalRoomRepository,
            IRepository<DrugStock> drugStockRepository,
            IRepository<Doctor> doctorRepository)
        {
            this.medicalRoomRepository = medicalRoomRepository;
            this.drugStockRepository = drugStockRepository;
            this.doctorRepository = doctorRepository;   
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(medicalRoomRepository.All());
        }

        [HttpPost]
        public IActionResult Create([FromBody] CreateMedicalRoomDto dto)
        {
            var medicalRoom = new MedicalRoom(dto.Adress);
            var drugStock = new DrugStock();
            medicalRoom.RegisterDrugStock(drugStock);

            medicalRoomRepository.Add(medicalRoom);
            drugStockRepository.Add(drugStock);

            medicalRoomRepository.SaveChanges();
            drugStockRepository.SaveChanges();

            return NoContent();
        }

        //[HttpPost("{medicalRoomId:guid}/{drugStockId:guid}")]
        //public IActionResult RegisterDrugstock(Guid medicalRoomId, Guid drugStockId)
        //{
        //    var medicalRoom = medicalRoomRepository.Get(medicalRoomId);
        //    if (medicalRoom == null)
        //    {
        //        return NotFound();
        //    }
        //    var drugStock = drugStockRepository.Get(drugStockId);
        //    if (drugStock == null)
        //    {
        //        return NotFound();
        //    }
        //    medicalRoom.RegisterDrugStock(drugStock);
        //    drugStock.AttachMedicalRoom(medicalRoom);
        //    drugStockRepository.SaveChanges();
        //    medicalRoomRepository.SaveChanges();
        //    return NoContent();
        //}
        [HttpPost("{medicalRoomId:guid}/doctors")]
        public IActionResult RegisterDoctors(Guid medicalRoomId, [FromBody] List<Guid> doctorsIds)
        {
            var medicalRoom = medicalRoomRepository.Get(medicalRoomId);
            if (medicalRoom == null)
            {
                return NotFound();
            }

            List<Doctor> doctors = new List<Doctor>();
            for(int i=0; i < doctorsIds.Count; i++)
            {
                var doctor = doctorRepository.Get(doctorsIds[i]);
                if (doctor == null)
                {
                    return NotFound();
                }
                doctors.Add(doctor);
            }
            medicalRoom.RegisterDoctors(doctors);

            doctorRepository.SaveChanges();
            medicalRoomRepository.SaveChanges();

            return NoContent();
        }
    }
}
