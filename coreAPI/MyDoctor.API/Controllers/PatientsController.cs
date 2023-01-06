using Microsoft.AspNetCore.Mvc;
using MyDoctor.API.DTOs;
using MyDoctor.API.Helpers;
using MyDoctorApp.Domain.Models;
using MyDoctorApp.Infrastructure.Generics;

namespace MyDoctor.API.Controllers
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("1.0")]
    public class PatientsController : ControllerBase
    {
        public const string UsedEmailError = "The email is already used!";
        public const string InvalidEmailError = "The email is invalid!";
        private const string CouldNotCreatePatientError = "Could not create a patient from the dto.";
        private const string InvalidPatientIdError = "There doesn't exist such patient with this id.";
        private readonly IRepository<Patient> patientRepository;
        private readonly IRepository<SurveyQuestion> surveyQuestionRepository;
        private readonly IRepository<Doctor> doctorRepository;

        public PatientsController(IRepository<Patient> patientRepository,
            IRepository<SurveyQuestion> surveyQuestionsRepository,
            IRepository<Doctor> doctorRepository)
        {
            this.patientRepository = patientRepository;
            this.surveyQuestionRepository = surveyQuestionsRepository;
            this.doctorRepository = doctorRepository;
        }

        private static List<SurveyQuestion> GenerateSurveyQuestions()
        {
            var surveyQuestions = new List<SurveyQuestion>();
            foreach (SurveyQuestion.Question question in Enum.GetValues(typeof(SurveyQuestion.Question)))
            {
                surveyQuestions.Add(new SurveyQuestion(SurveyQuestion.GetQuestionBody(question)));
            }
            return surveyQuestions;
        }
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok((await patientRepository.AllAsync()).Select(p => patientRepository.GetMapper().Map<DisplayPatientDto>(p)));
        }

        [HttpGet("{patientId:guid}")]
        public async Task<IActionResult> GetById(Guid patientId)
        {
            var patient = await patientRepository.GetAsync(patientId);
            return patient == null ? NotFound(InvalidPatientIdError) : Ok(patientRepository.GetMapper().Map<DisplayPatientDto>(patient));
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreatePatientDto dto)
        {
            var ActionResultPatientTuple = await CreatePatientFromDto(dto);

            if (ActionResultPatientTuple.Item2.GetType() != typeof(OkResult))
                return ActionResultPatientTuple.Item2;

            if (ActionResultPatientTuple.Item1 == null)
                return BadRequest(CouldNotCreatePatientError);

            Patient patient = ActionResultPatientTuple.Item1;
            var surveyQuestions = GenerateSurveyQuestions();
            patient.RegisterSurveyQuestions(surveyQuestions);
            surveyQuestions.ForEach(async q => await surveyQuestionRepository.AddAsync(q));
            await patientRepository.AddAsync(patient);

            await surveyQuestionRepository.SaveChangesAsync();
            await patientRepository.SaveChangesAsync();
            return Ok(patientRepository.GetMapper().Map<DisplayPatientDto>(patient));
        }



        [HttpPut("{patientId:guid}")]
        public async Task<IActionResult> Update(Guid patientId, [FromBody] UpdateUserDto dto)
        {
            var patient = await patientRepository.GetAsync(patientId);
            if (patient == null)
            {
                return NotFound();
            }

            var patientNew = new Patient(patient.Email, patient.Password, dto.FirstName, dto.LastName, dto.Description);

            patient.Update(patientNew);

            patientRepository.Update(patient);

            await patientRepository.SaveChangesAsync();
            return Ok(patientRepository.GetMapper().Map<DisplayPatientDto>(patient));
        }

        [HttpDelete("{patientId:guid}")]
        public async Task<IActionResult> Delete(Guid patientId)
        {
            try
            {
                await patientRepository.Delete(patientId);
            }
            catch (ArgumentException)
            {
                return BadRequest(InvalidPatientIdError);
            }

            await patientRepository.SaveChangesAsync();
            return Ok();
        }

        private async Task<(Patient?, IActionResult)> CreatePatientFromDto(CreatePatientDto dto)
        {
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
            var newPatient = new Patient(dto.UserDetails.Email, hashedPassword, dto.UserDetails.FirstName, dto.UserDetails.LastName);

            return (newPatient, Ok());
        }
    }
}
