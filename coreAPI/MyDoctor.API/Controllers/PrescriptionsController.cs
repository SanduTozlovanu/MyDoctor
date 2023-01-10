using Microsoft.AspNetCore.Mvc;
using MyDoctor.API.DTOs;
using MyDoctorApp.Domain.Models;
using MyDoctorApp.Infrastructure.Generics;

namespace MyDoctor.API.Controllers
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("1.0")]
    public class PrescriptionsController : ControllerBase
    {
        public const string AppointmentNotFoundError = "Could not find an appointment with this Id.";
        public const string DoctorNotFoundError = "Could not find the doctor in the DataBase";
        public const string DrugStockNotFoundError = "Could not find the drugStock in the DataBase";
        public const string DrugCreateError = "Could not create drugs.";
        public const string NoPrescriptionFoundError = "There is no prescription with this appointmentId";
        public const string TooManyDrugsTakenError = "You tried to take more drugs than it's available.";
        public const string BillNotFoundError = "Could not find the bill of the appointment with this Id.";
        private const string PrescriptionAlreadyCreatedError = "The appointment with that id has already a prescription";
        private readonly IRepository<Prescription> prescriptionRepository;
        private readonly IRepository<Appointment> appointmentRepository;
        private readonly IRepository<DrugStock> drugStockRepository;
        private readonly IRepository<Drug> drugRepository;
        private readonly IRepository<Doctor> doctorRepository;
        private readonly IRepository<Procedure> procedureRepository;
        private readonly IRepository<Bill> billRepository;
        private readonly IRepository<PrescriptedDrug> prescriptedDrugRepository;
        public PrescriptionsController(IRepository<Prescription> prescriptionRepository,  //NOSONAR
                                      IRepository<Appointment> appointmentRepository,
                                      IRepository<DrugStock> drugStockRepository,
                                      IRepository<Drug> drugRepository,
                                      IRepository<Doctor> doctorRepository,
                                      IRepository<Procedure> procedureRepository,
                                      IRepository<Bill> billRepository,
                                      IRepository<PrescriptedDrug> prescriptedDrugRepository)
        {
            this.prescriptionRepository = prescriptionRepository;
            this.appointmentRepository = appointmentRepository;
            this.drugStockRepository = drugStockRepository;
            this.drugRepository = drugRepository;
            this.doctorRepository = doctorRepository;
            this.procedureRepository = procedureRepository;
            this.billRepository = billRepository;
            this.prescriptedDrugRepository = prescriptedDrugRepository;
        }

        [HttpGet("{appointmentId:guid}")]
        public async Task<IActionResult> GetByAppointmentId(Guid appointmentId)
        {
            var prescription = (await prescriptionRepository.FindAsync(p => p.AppointmentId == appointmentId)).FirstOrDefault();
            if(prescription == null) 
            {
                return NotFound(NoPrescriptionFoundError);
            }
            var prescriptedDrugs = (await prescriptedDrugRepository.FindAsync(pd => pd.PrescriptionId == prescription.Id)).ToList();
            var procedures = (await procedureRepository.FindAsync(p => p.PrescriptionId == prescription.Id)).ToList();
            prescription.RegisterProcedures(procedures);
            prescription.RegisterPrescriptedDrugs(prescriptedDrugs);
            return Ok(prescriptionRepository.GetMapper().Map<DisplayPrescriptionDto>(prescription));
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
        [HttpPost("{appointmentId:guid}")]
        public async Task<IActionResult> Create(Guid appointmentId, [FromBody] CreatePrescriptionDto dto)
        {
            if((await prescriptionRepository.FindAsync(p => p.AppointmentId == appointmentId)).FirstOrDefault() is not null)
            {
                return BadRequest(PrescriptionAlreadyCreatedError);
            }
            Prescription prescription = new(dto.Description, dto.Name);
            Appointment? appointment = await appointmentRepository.GetAsync(appointmentId);
            if (appointment == null)
            {
                return NotFound(AppointmentNotFoundError);
            }
            Doctor? doctor = await doctorRepository.GetAsync(appointment.DoctorId);
            if (doctor == null)
            {
                return NotFound(DoctorNotFoundError);
            }
            if (dto.Procedures != null && dto.Procedures.Any())
            {
                List<Procedure> procedures = dto.Procedures.Select(procDto => procedureRepository.GetMapper().Map<Procedure>(procDto)).ToList();
                prescription.RegisterProcedures(procedures);
                procedures.ForEach(async p => await procedureRepository.AddAsync(p));
            }
            if (dto.Drugs != null && dto.Drugs.Any())
            {
                var result = await AttachPrescriptedDrugsToPrescription(doctor, prescription, dto.Drugs);
                if (result.GetType() != typeof(OkResult))
                {
                    return result;
                }
            }
            // Register prescription after done with it, because it needs to already have all the drugs to make the billing before registration.
            appointment.RegisterPrescription(prescription);
            await prescriptionRepository.AddAsync(prescription);

            Bill? bill = (await billRepository.FindAsync(b => b.AppointmentId == appointment.Id)).FirstOrDefault();
            if (bill == null)
            {
                return NotFound(BillNotFoundError);
            }
            appointment.RegisterBill(bill);
            billRepository.Update(bill);

            await prescriptedDrugRepository.SaveChangesAsync();
            await billRepository.SaveChangesAsync();
            await procedureRepository.SaveChangesAsync();
            await drugRepository.SaveChangesAsync();
            await prescriptionRepository.SaveChangesAsync();

            return Ok(prescriptionRepository.GetMapper().Map<DisplayPrescriptionDto>(prescription));
        }

        private async Task<IActionResult> AttachPrescriptedDrugsToPrescription(Doctor doctor, Prescription prescription, List<GetDrugDto> dtos)
        {
            DrugStock? drugStock = (await drugStockRepository.FindAsync(ds => ds.MedicalRoomId == doctor.MedicalRoomId)).FirstOrDefault();
            if (drugStock == null)
            {
                return NotFound(DrugStockNotFoundError);
            }
            bool drugNotFound = false;
            List<Tuple<Guid, uint>> getDrugTuples = dtos.Select(dto => Tuple.Create(dto.DrugId, dto.Quantity)).ToList();
            List<Tuple<Drug, uint>> drugTuples = new();

            foreach (var tuple in getDrugTuples)
            {
                var drugId = tuple.Item1;
                var quantityToTake = tuple.Item2;
                var drug = await drugRepository.GetAsync(drugId);
                if (drug == null || drug.DrugStockId != drugStock.Id)
                {
                    drugNotFound = true;
                    break;
                }
                drugTuples.Add(Tuple.Create(drug, quantityToTake));
            }
            if (drugNotFound)
            {
                return NotFound(DrugCreateError);
            }

            foreach (var tuple in drugTuples)
            {
                Drug drug = tuple.Item1;
                var quantityToTake = tuple.Item2;

                if (drug.GetDrugs(quantityToTake).IsFailure)
                {
                    return BadRequest(TooManyDrugsTakenError);
                }
                drugRepository.Update(drug);
            }
            List<PrescriptedDrug> prescriptedDrugs = new();
            drugTuples.ForEach(drugTuple =>
            {
                PrescriptedDrug prescriptedDrug = new(drugTuple.Item2);
                prescriptedDrug.AttachDrug(drugTuple.Item1);
                prescriptedDrugs.Add(prescriptedDrug);
            });

            prescription.RegisterPrescriptedDrugs(prescriptedDrugs);
            prescriptedDrugs.ForEach(async prescriptedDrug => await prescriptedDrugRepository.AddAsync(prescriptedDrug));
            return Ok();
        }
    }
}