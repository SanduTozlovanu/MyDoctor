using MediatR;
using Microsoft.AspNetCore.Mvc;
using MyDoctor.API.DTOs;
using MyDoctor.API.Helpers;
using MyDoctor.Application.Queries.GetDoctorAvailableAppointmentsQueries;
using MyDoctorApp.Domain.Helpers;
using MyDoctorApp.Domain.Models;
using MyDoctorApp.Infrastructure.Generics;

namespace MyDoctor.API.Controllers
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("1.0")]
    public class DoctorsController : ControllerBase
    {
        public const string SpecialityNotFoundError = "Could not find specialty by the specialityId provided.";
        public const string FreeMedicalRoomNotFoundError = "Could not find a free medical room for this doctor.";
        public const string MedicalRoomNotFoundError = "Could not find a medicalRoom with this Id.";
        private const string ImageProcessError = "An error occured during processing the images - ";
        private const string ProfilePhotoFolderName = "profilePhotos";
        private const string DiplomaPhotoFolderName = "diplomaPhotos";
        private const string ResourcesFolderName = "resources";
        private const string MissingPhotoFileName = "missingPhoto.jpg";
        private const string BackPath = "..";
        public const string UsedEmailError = "The email is already used!";
        public const string InvalidEmailError = "The email is invalid!";
        public const string CouldNotCreateDoctorError = "Could not create a doctor from the dto.";
        private const string InvalidDoctorIdError = "There is no such Doctor with this id.";
        private const string SuccessfulPhotosMessage = "Photos have been updated successfully";
        private readonly List<string> possiblePhotoExtensions = new() { "jpg", "png", "jpeg" };
        private readonly IRepository<Doctor> doctorRepository;
        private readonly IRepository<MedicalRoom> medicalRoomRepository;
        private readonly IRepository<Patient> patientRepository;
        private readonly IRepository<Speciality> specialityRepository;
        private readonly IRepository<ScheduleInterval> scheduleIntervalRepository;
        private readonly IMediator mediator;

        public DoctorsController(IMediator mediator, IRepository<Doctor> doctorRepository,
            IRepository<MedicalRoom> medicalRoomRepository,
            IRepository<Patient> patientRepository,
            IRepository<Speciality> specialityRepository,
            IRepository<ScheduleInterval> scheduleIntervalRepository)
        {
            this.mediator = mediator;
            this.doctorRepository = doctorRepository;
            this.medicalRoomRepository = medicalRoomRepository;
            this.patientRepository = patientRepository;
            this.specialityRepository = specialityRepository;
            this.scheduleIntervalRepository = scheduleIntervalRepository;
        }

        private static List<ScheduleInterval> GenerateScheduleIntervals()
        {
            var scheduleIntervals = new List<ScheduleInterval>();
            foreach (var day in Enum.GetNames(typeof(WeekDays)))
            {
                scheduleIntervals.Add(new ScheduleInterval(day, new TimeOnly(6, 0), new TimeOnly(20, 00)));
            }
            return scheduleIntervals;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok((await doctorRepository.AllAsync()).Select(d => doctorRepository.GetMapper().Map<DisplayDoctorDto>(d)));
        }

        [HttpGet("get_by_speciality/{specialityId:guid}")]
        public async Task<IActionResult> GetBySpeciality(Guid specialityId)
        {
            Speciality? speciality = await specialityRepository.GetAsync(specialityId);
            if (speciality == null)
            {
                return NotFound(SpecialityNotFoundError);
            }

            var doctors = await doctorRepository.FindAsync(d => d.SpecialityID == specialityId);

            return Ok(doctors.Select(d => doctorRepository.GetMapper().Map<DisplayDoctorDto>(d)));
        }

        [HttpGet("profilePhoto/{doctorId:guid}")]
        public async Task<IActionResult> GetProfilePhoto(Guid doctorId)
        {
            var doctor = await doctorRepository.GetAsync(doctorId);
            if (doctor == null)
            {
                return NotFound(InvalidDoctorIdError);
            }
            string fileExtension = "";
            possiblePhotoExtensions.ForEach(extension =>
            {
                if (new FileInfo(Path.Combine(Directory.GetCurrentDirectory(), BackPath, ProfilePhotoFolderName, $"{doctor.Email}.{extension}")).Exists)
                {
                    fileExtension = extension;
                    return;
                }
            });
            if (fileExtension.Length == 0)
            {
                return PhysicalFile(Path.Combine(Directory.GetCurrentDirectory(), BackPath, ResourcesFolderName, MissingPhotoFileName), "image/jpg");
            }
            try
            {
                return PhysicalFile(Path.Combine(Directory.GetCurrentDirectory(), BackPath, ProfilePhotoFolderName, $"{doctor.Email}.{fileExtension}"), $"image/{fileExtension}");
            }
            catch (Exception ex)
            {
                return BadRequest(ImageProcessError + ex);
            }
        }
        /// <summary>
        /// Endpoint for getting the available appointments for a doctor for a specific date
        /// </summary>
        /// <remarks>
        /// Parameters remarks
        /// 
        ///     date field format is: "yyyy-mm-d"
        ///         example: "date" : "2023-12-24"
        ///         
        /// </remarks>
        [HttpGet("get_available_appointment_schedule/{doctorId:guid}")]
        public async Task<IActionResult> GetAvailableAppointmentSchedules(Guid doctorId, DateOnly date)
        {
            var result = await mediator.Send(new GetDoctorAvailableAppointmentsQuery(doctorId, date));
            return Ok(result);
        }

        [HttpPost("speciality")]
        public async Task<IActionResult> Create([FromBody] CreateDoctorDto dto)
        {
            var specialityId = dto.SpecialityId;
            Speciality? speciality = await specialityRepository.GetAsync(specialityId);
            if (speciality == null)
            {
                return NotFound(SpecialityNotFoundError);
            }

            List<MedicalRoom> medicalRooms = (await medicalRoomRepository.AllAsync()).ToList();
            (MedicalRoom?, int) medicalRoomWithFewestDoctors = new(null, int.MaxValue);
            medicalRooms.ForEach(async mr =>
            {
                int doctorNumber = (await doctorRepository.FindAsync(d => mr.Id == d.MedicalRoomId)).Count();
                if (doctorNumber < medicalRoomWithFewestDoctors.Item2)
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


            var scheduleIntervals = GenerateScheduleIntervals();
            foreach (var scheduleInterval in scheduleIntervals)
            {
                await scheduleIntervalRepository.AddAsync(scheduleInterval);
            }

            var ActionResultDoctorTuple = await CreateDoctorFromDto(dto);

            if (ActionResultDoctorTuple.Item2.GetType() != typeof(OkResult))
                return ActionResultDoctorTuple.Item2;

            if (ActionResultDoctorTuple.Item1 == null)
                return BadRequest(ActionResultDoctorTuple);

            Doctor doctor = ActionResultDoctorTuple.Item1;
            medicalRoom.RegisterDoctors(new List<Doctor> { doctor });
            doctor.RegisterScheduleIntervals(scheduleIntervals);
            speciality.RegisterDoctor(doctor);

            await doctorRepository.AddAsync(doctor);
            await doctorRepository.SaveChangesAsync();
            await medicalRoomRepository.SaveChangesAsync();
            await specialityRepository.SaveChangesAsync();
            await scheduleIntervalRepository.SaveChangesAsync();

            return Ok(doctorRepository.GetMapper().Map<DisplayDoctorDto>(doctor));
        }

        [HttpPut("photos/{doctorId:guid}")]
        public async Task<IActionResult> UpdatePhotos(Guid doctorId, [FromForm] UpdateDoctorPhotosDto dto)
        {
            var doctor = await doctorRepository.GetAsync(doctorId);
            if (doctor == null)
            {
                return NotFound();
            }
            try
            {
                possiblePhotoExtensions.ForEach(extension =>
                {
                    var profileFile = new FileInfo(Path.Combine(Directory.GetCurrentDirectory(), BackPath, ProfilePhotoFolderName, $"{doctor.Email}.{extension}"));
                    if (profileFile.Exists)
                    {
                        profileFile.Delete();
                    }

                    var diplomaFile = new FileInfo(Path.Combine(Directory.GetCurrentDirectory(), BackPath, DiplomaPhotoFolderName, $"{doctor.Email}.{extension}"));
                    if (diplomaFile.Exists)
                    {
                        diplomaFile.Delete();
                    }
                });
                var profilePhotoPath = Path.Combine(Directory.GetCurrentDirectory(), BackPath, ProfilePhotoFolderName, $"{doctor.Email}{new FileInfo(dto.ProfilePhoto.FileName).Extension}");
                var diplomaPhotoPath = Path.Combine(Directory.GetCurrentDirectory(), BackPath, DiplomaPhotoFolderName, $"{doctor.Email}{new FileInfo(dto.DiplomaPhoto.FileName).Extension}");

                var profileStream = new FileStream(profilePhotoPath, FileMode.Create);
                var diplomaStream = new FileStream(diplomaPhotoPath, FileMode.Create);
                await dto.ProfilePhoto.CopyToAsync(profileStream);
                await dto.DiplomaPhoto.CopyToAsync(diplomaStream);
            }
            catch (Exception ex)
            {
                return BadRequest(ImageProcessError + ex);
            }
            return Ok(SuccessfulPhotosMessage);
        }

        [HttpPut("{doctorId:guid}")]
        public async Task<IActionResult> Update(Guid doctorId, [FromBody] UpdateDoctorDto dto)
        {
            var doctor = await doctorRepository.GetAsync(doctorId);
            if (doctor == null)
            {
                return NotFound();
            }

            var doctorNew = new Doctor(doctor.Email, doctor.Password, dto.UpdateUserDto.FirstName,
                dto.UpdateUserDto.LastName, dto.AppointmentPrice, dto.UpdateUserDto.Description, dto.UpdateUserDto.Username);

            doctor.Update(doctorNew);

            doctorRepository.Update(doctor);

            await doctorRepository.SaveChangesAsync();
            return Ok(doctorRepository.GetMapper().Map<DisplayDoctorDto>(doctor));
        }


        [HttpDelete("{doctorId:guid}")]
        public async Task<IActionResult> Delete(Guid doctorId)
        {
            try
            {
                await doctorRepository.Delete(doctorId);
            }
            catch (ArgumentException)
            {
                return BadRequest(InvalidDoctorIdError);
            }

            await doctorRepository.SaveChangesAsync();
            return Ok();
        }

        private async Task<(Doctor?, IActionResult)> CreateDoctorFromDto(CreateDoctorDto dto)
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
            var newDoctor = new Doctor(dto.UserDetails.Email, hashedPassword, dto.UserDetails.FirstName, dto.UserDetails.LastName);

            return (newDoctor, Ok());
        }
    }
}
