﻿using Microsoft.AspNetCore.Mvc;
using MyDoctor.API.DTOs;
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

        public MedicalRoomController(IRepository<MedicalRoom> medicalRoomRepository,
            IRepository<DrugStock> drugStockRepository)
        {
            this.medicalRoomRepository = medicalRoomRepository;
            this.drugStockRepository = drugStockRepository;
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(medicalRoomRepository.All().Select(mr => new DisplayMedicalRoomDto(mr.Id, mr.Adress)));
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

            return Ok(new DisplayMedicalRoomDto(medicalRoom.Id, medicalRoom.Adress));
        }
    }
}
