using Microsoft.AspNetCore.Mvc;
using MyDoctor.API.DTOs;
using MyDoctor.API.Helpers;
using MyDoctorApp.Domain.Models;
using MyDoctorApp.Infrastructure.Generics;

namespace MyDoctor.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PatientController : ControllerBase
    {
        public const string UsedEmailError = "The email is already used!";
        public const string InvalidEmailError = "The email is invalid!";
        public const string BigAgeError = "Too big age value.";
        private const string CouldNotCreatePatientError = "Could not create a patient from the dto.";
        private readonly IRepository<Patient> patientRepository;
        private readonly IRepository<MedicalHistory> medicalHistoryRepository;
        private readonly IRepository<Doctor> doctorRepository;

        public PatientController(IRepository<Patient> patientRepository,
            IRepository<MedicalHistory> medicalHistoryRepository,
            IRepository<Doctor> doctorRepository)
        {
            this.patientRepository = patientRepository;
            this.medicalHistoryRepository = medicalHistoryRepository;
            this.doctorRepository = doctorRepository;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok((await patientRepository.AllAsync()).Select(p => patientRepository.GetMapper().Map<DisplayPatientDto>(p)));
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreatePatientDto dto)
        {
            var ActionResultPatientTuple = await CreatePatientFromDto(dto);

            if (ActionResultPatientTuple.Item2.GetType() != typeof(OkResult))
                return ActionResultPatientTuple.Item2;

            if (ActionResultPatientTuple.Item1 == null)
                return BadRequest(CouldNotCreatePatientError);

            var medicalHistory = new MedicalHistory();
            Patient patient = ActionResultPatientTuple.Item1;

            patient.RegisterMedicalHistory(medicalHistory);

            await medicalHistoryRepository.AddAsync(medicalHistory);
            await patientRepository.AddAsync(patient);

            await medicalHistoryRepository.SaveChangesAsync();
            await patientRepository.SaveChangesAsync();
            return Ok(patientRepository.GetMapper().Map<DisplayPatientDto>(patient));
        }



        [HttpPut("{patientId:guid}")]
        public async Task<IActionResult> Update(Guid patientId, [FromBody] CreatePatientDto dto)
        {
            var patient = await patientRepository.GetAsync(patientId);
            if (patient == null)
            {
                return NotFound();
            }

            var ActionResultPatientTuple = await CreatePatientFromDto(dto);

            if (ActionResultPatientTuple.Item2.GetType() != typeof(OkResult))
                return ActionResultPatientTuple.Item2;

            if (ActionResultPatientTuple.Item1 == null)
                return BadRequest(CouldNotCreatePatientError);

            patient.Update(ActionResultPatientTuple.Item1);

            patientRepository.Update(patient);

            await patientRepository.SaveChangesAsync();
            return Ok();
        }

        [HttpDelete("{patientId:guid}")]
        public async Task<IActionResult> Delete(Guid patientId)
        {
            var patient = await patientRepository.GetAsync(patientId);
            if (patient == null)
            {
                return NotFound();
            }

            patientRepository.Delete(patient);

            await patientRepository.SaveChangesAsync();
            return Ok();
        }

        private async Task<(Patient?, IActionResult)> CreatePatientFromDto(CreatePatientDto dto)
        {
            if (dto.Age > 120) return (null, BadRequest(BigAgeError));

            var oldPatient = (await patientRepository.FindAsync(p => p.Email == dto.UserDetails.Email)).FirstOrDefault();
            var oldDoctor = (await doctorRepository.FindAsync(d => d.Email == dto.UserDetails.Email)).FirstOrDefault();
            if (oldPatient != null || oldDoctor != null)
            {
                return (null, BadRequest(UsedEmailError));
            }

            if (!AccountInfoManager.ValidateEmail(dto.UserDetails.Email))
            {
                return (null, BadRequest(InvalidEmailError));
            }

            string hashedPassword = AccountInfoManager.HashPassword(dto.UserDetails.Password);
            var newPatient = new Patient(dto.UserDetails.Email, hashedPassword, dto.UserDetails.FirstName, dto.UserDetails.LastName, dto.Age);

            return (newPatient, Ok());
        }
    }
}
