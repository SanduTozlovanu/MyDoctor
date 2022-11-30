﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyDoctor.API.DTOs;
using MyDoctor.Domain.Models;
using MyDoctorApp.Infrastructure.Generics;
using MyDoctorApp.Infrastructure.Generics.GenericRepositories;

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
            return Ok(billRepository.All());
        }
    }


}