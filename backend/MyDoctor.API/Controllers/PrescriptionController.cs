using Microsoft.AspNetCore.Mvc;
using MyDoctor.API.DTOs;
using MyDoctor.Domain.Models;
using MyDoctorApp.Infrastructure.Generics;

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
        private readonly IRepository<Doctor> doctorRepository;
        private readonly IRepository<Procedure> procedureRepository;
        private readonly IRepository<Bill> billRepository;
        private readonly IRepository<PrescriptedDrug> prescriptedDrugRepository;

        public PrescriptionController(IRepository<Prescription> prescriptonRepository,
            IRepository<Appointment> appointmentRepository,
            IRepository<DrugStock> drugStockRepository,
            IRepository<Drug> drugRepository,
            IRepository<Doctor> doctorRepository,
            IRepository<Procedure> procedureRepository,
            IRepository<Bill> billRepository,
            IRepository<PrescriptedDrug> prescriptedDrugRepository)
        {
            this.prescriptonRepository = prescriptonRepository;
            this.appointmentRepository = appointmentRepository;
            this.drugStockRepository = drugStockRepository;
            this.drugRepository = drugRepository;
            this.doctorRepository = doctorRepository;
            this.procedureRepository = procedureRepository;
            this.billRepository = billRepository;
            this.prescriptedDrugRepository = prescriptedDrugRepository;
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(prescriptonRepository.All().Select(p => new DisplayPrescriptionDto(p.Id, p.AppointmentId, p.Description, p.Name)));
        }

        /// <summary>
        /// Endpoint for creating a full prescription
        /// </summary>
        /// <remarks>
        /// Parameters remarks
        /// 
        ///     procedures and drugs fields are optional.
        ///     
        /// </remarks>
        [HttpPost("{appointmentId:guid}/create_prescription")]
        public IActionResult Create(Guid appointmentId, [FromBody] CreatePrescriptionDto dto)
        {
            Prescription prescription = new Prescription(dto.Description, dto.Name);
            Appointment appointment = appointmentRepository.Get(appointmentId);
            if (appointment == null)
            {
                return NotFound("Could not find an appointment with this Id.");
            }
            if (dto.procedures.Any())
            {
                List<Procedure> procedures = dto.procedures.Select(dto => new Procedure(dto.Name, dto.Description, dto.Price)).ToList();
                prescription.RegisterProcedures(procedures);
                procedures.ForEach(p => procedureRepository.Add(p));
            }
            if (dto.drugs.Any())
            {
                Doctor doctor = doctorRepository.Get(appointment.DoctorId);
                if (doctor == null)
                {
                    return NotFound("Could not find the doctor in the DataBase");
                }
                DrugStock? drugStock = drugStockRepository.Find(ds => ds.MedicalRoomId == doctor.MedicalRoomId).FirstOrDefault();
                if (drugStock == null) 
                {
                    return NotFound("Could not find the drugStock in the DataBase");
                }
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
                    return NotFound("Could not create drugs.");
                }

                List<Drug> drugs = new List<Drug>();
                foreach (var tuple in drugTuples)
                {
                    Drug drug = tuple.Item1;
                    var quantityToTake = tuple.Item2;

                    if (drug.GetDrugs(quantityToTake).IsFailure)
                    {
                        return BadRequest("You tried to take more drugs than it's available.");
                    }
                    drugRepository.Update(drug);

                    drugs.Add(drug);
                }
                List<PrescriptedDrug> prescriptedDrugs= new List<PrescriptedDrug>();
                drugTuples.ForEach(drugTuple => 
                {
                    PrescriptedDrug prescriptedDrug = new PrescriptedDrug(drugTuple.Item2);
                    prescriptedDrug.AttachDrug(drugTuple.Item1);
                    prescriptedDrugs.Add(prescriptedDrug);
                });

                prescription.RegisterPrescriptedDrugs(prescriptedDrugs);
                prescriptedDrugs.ForEach(prescriptedDrug => prescriptedDrugRepository.Add(prescriptedDrug));
            }
            // Register prescription after done with it, because it needs to already have all the drugs to make the billing before registration.
            appointment.RegisterPrescription(prescription);
            prescriptonRepository.Add(prescription);

            Bill? bill = billRepository.Find(b => b.AppointmentId == appointment.Id).FirstOrDefault();
            if (bill == null)
            {
                return NotFound("Could not find the bill of the appointment with this Id.");
            }
            appointment.RegisterBill(bill);
            billRepository.Update(bill);

            prescriptedDrugRepository.SaveChanges();
            billRepository.SaveChanges();
            procedureRepository.SaveChanges();
            drugRepository.SaveChanges();
            prescriptonRepository.SaveChanges();

            return Ok(new DisplayPrescriptionDto(prescription.Id, prescription.AppointmentId, prescription.Description, prescription.Name));
        }
    }
}
