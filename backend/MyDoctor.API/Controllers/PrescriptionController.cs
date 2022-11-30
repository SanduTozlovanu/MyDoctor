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
    public class PrescriptionController : ControllerBase
    {
        private readonly IRepository<Prescription> prescriptonRepository;
        private readonly IRepository<Appointment> appointmentRepository;
        private readonly IRepository<DrugStock> drugStockRepository;
        private readonly IRepository<Drug> drugRepository;

        public PrescriptionController(IRepository<Prescription> prescriptonRepository,
            IRepository<Appointment> appointmentRepository,
            IRepository<DrugStock> drugStockRepository,
            IRepository<Drug> drugRepository)
        {
            this.prescriptonRepository = prescriptonRepository;
            this.appointmentRepository = appointmentRepository;
            this.drugStockRepository = drugStockRepository;
            this.drugRepository = drugRepository;
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(prescriptonRepository.All());
        }

        [HttpPost("{appointmentId:guid}/create_prescription")]
        public IActionResult Create(Guid appointmentId, [FromBody] CreatePrescriptionDto dto)
        {

            var appointment = appointmentRepository.Get(appointmentId);

            if (appointment == null)
            {
                return NotFound("Could not find an appointment with this Id.");
            }

            var drugStock = appointment.Doctor.MedicalRoom.DrugStock;
            bool drugNotFound = false;
            List<Tuple<Guid, uint>> getDrugTuples = dto.drugs.Select(dto => Tuple.Create(dto.drugId, dto.Quantity)).ToList();
            List<Tuple<Drug, uint>> drugTuples = new List<Tuple<Drug, uint>>();

            foreach (var tuple in getDrugTuples)
            {
                var drugId = tuple.Item1;
                var quantityToTake = tuple.Item2;
                var drug = drugRepository.Get(drugId);
                if (drug == null)
                {
                    drugNotFound = true;
                    break;
                }
                else
                {
                    drugTuples.Add(Tuple.Create(drug, quantityToTake));
                }
            }


            if (drugStock == null)
            {
                return NotFound("Could not find a drugStock with this Id.");
            }
            if (drugNotFound == true)
            {
                return NotFound("Could create drugs.");
            }


            var prescription = new Prescription(dto.Description, dto.Name);

            List<Drug> drugs = new List<Drug>();
            foreach (var tuple in drugTuples)
            {
                Drug drug = tuple.Item1;
                var quantityToTake = tuple.Item2;

                if (drug.GetDrugs(quantityToTake).IsFailure)
                {
                    return BadRequest("You tried to take more drugs than it's available.");
                }

                drugs.Add(drug);
            }
            prescription.RegisterDrugs(drugs);


            // Register prescription after done with it, because it needs to already have all the drugs to make the billing before registration.
            appointment.RegisterPrescription(prescription);

            prescriptonRepository.Add(prescription);

            drugRepository.SaveChanges();
            drugStockRepository.SaveChanges();
            prescriptonRepository.SaveChanges();
            appointmentRepository.SaveChanges();

            return Ok(new { id = prescription.Id });
        }
    }
}
