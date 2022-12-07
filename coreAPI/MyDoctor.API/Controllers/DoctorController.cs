using Microsoft.AspNetCore.Mvc;
using MyDoctor.API.DTOs;
using MyDoctor.API.Helpers;
using MyDoctor.Domain.Models;
using MyDoctorApp.Infrastructure.Generics;
using MyDoctorApp.Infrastructure.Generics.GenericRepositories;

namespace MyDoctor.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DoctorController : ControllerBase
    {
        public const string FreeMedicalRoomNotFoundError = "Could not find a free medical room for this doctor.";
        public const string MedicalRoomNotFoundError = "Could not find a medicalRoom with this Id.";
        public const string UsedEmailError = "The email is already used!";
        public const string InvalidEmailError = "The email is invalid!";
        private readonly IRepository<Doctor> doctorsRepository;
        private readonly IRepository<MedicalRoom> medicalRoomRepository;
        private readonly IRepository<Patient> patientsRepository;

        public DoctorController(IRepository<Doctor> doctorsRepository,
            IRepository<MedicalRoom> medicalRoomRepository,
            IRepository<Patient> patientsRepository)
        {
            this.doctorsRepository = doctorsRepository;
            this.medicalRoomRepository = medicalRoomRepository;
            this.patientsRepository = patientsRepository;
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(doctorsRepository.All().Select(d => new DisplayDoctorDto(d.Id, d.MedicalRoomId, d.Email, d.Speciality, d.FirstName, d.LastName)));
        }
        [HttpPost]
        public IActionResult Create([FromBody] CreateDoctorDto dto)
        {
            List<MedicalRoom> medicalRooms = medicalRoomRepository.All().ToList();
            (MedicalRoom?, int) medicalRoomWithFewestDoctors = new(null, int.MaxValue);
            medicalRooms.ForEach(mr =>
            {
                int doctorNumber = doctorsRepository.Find(d => mr.Id == d.MedicalRoomId).Count();
                if(doctorNumber < medicalRoomWithFewestDoctors.Item2)
                    medicalRoomWithFewestDoctors = (mr, doctorNumber);

            });
            if (medicalRoomWithFewestDoctors.Item1 == null)
            {
                return NotFound(FreeMedicalRoomNotFoundError);
            }
            MedicalRoom medicalRoom = medicalRoomWithFewestDoctors.Item1;
            if (medicalRoom == null)
            {
                return NotFound(MedicalRoomNotFoundError);
            }

            var oldPatient = patientsRepository.Find(p => p.Email == dto.UserDetails.Email).FirstOrDefault();
            var oldDoctor = doctorsRepository.Find(d => d.Email == dto.UserDetails.Email).FirstOrDefault();
            if (oldPatient != null || oldDoctor != null)
            {
                return BadRequest(UsedEmailError);
            }

            if (!AccountInfoManager.ValidateEmail(dto.UserDetails.Email))
            {
                return BadRequest(InvalidEmailError);
            }


            string hashedPassword = AccountInfoManager.HashPassword(dto.UserDetails.Password);
            var doctor = new Doctor(dto.UserDetails.Email, hashedPassword, dto.UserDetails.FirstName, dto.UserDetails.LastName, dto.Speciality);
            medicalRoom.RegisterDoctors(new List<Doctor> { doctor });

            //if (dto.ProfilePhoto == null)
            //{
            //    return BadRequest("No profile photo has been loaded.");
            //}
            //if (dto.DiplomaPhoto == null)
            //{
            //    return BadRequest("No diploma photo has been loaded.");
            //}
            //try
            //{
            //    var profilePhotoPath = Path.Combine(Directory.GetCurrentDirectory(), "profilePhotos", dto.ProfilePhoto.FileName);
            //    var diplomaPhotoPath = Path.Combine(Directory.GetCurrentDirectory(), "diplomaPhotos", dto.DiplomaPhoto.FileName);

            //    var profileStream = new FileStream(profilePhotoPath, FileMode.Create);
            //    var diplomaStream = new FileStream(diplomaPhotoPath, FileMode.Create);
            //    dto.ProfilePhoto.CopyToAsync(profileStream);
            //    dto.DiplomaPhoto.CopyToAsync(diplomaStream);
            //}
            //catch (Exception ex)
            //{
            //    return BadRequest($"An error occured during processing the images - {ex}.");
            //}

            doctorsRepository.Add(doctor);
            doctorsRepository.SaveChanges();
            medicalRoomRepository.SaveChanges();

            return Ok(new DisplayDoctorDto(doctor.Id, doctor.MedicalRoomId, doctor.Email, doctor.Speciality, doctor.FirstName, doctor.LastName));
        }


        [HttpDelete("{doctorId:guid}")]
        public IActionResult Delete(Guid doctorId)
        {
            var doctor = doctorsRepository.Get(doctorId);
            if (doctor == null)
            {
                return NotFound();
            }

            doctorsRepository.Delete(doctor);

            doctorsRepository.SaveChanges();
            return Ok();
        }
    }
}
