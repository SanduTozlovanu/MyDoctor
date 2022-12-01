﻿using Microsoft.AspNetCore.Mvc;
using MyDoctor.API.DTOs;
using MyDoctor.Domain.Models;
using MyDoctorApp.Infrastructure.Generics;

namespace MyDoctor.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BillController : ControllerBase
    {
        private readonly IRepository<Bill> billRepository;

        public BillController(IRepository<Bill> billRepository)
        {
            this.billRepository = billRepository;
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(billRepository.All().Select(b => new DisplayBillDto(b.Id, b.AppointmentId, b.BillPrice)));
        }
    }


}
