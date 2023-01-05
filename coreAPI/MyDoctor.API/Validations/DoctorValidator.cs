using FluentValidation;
using MyDoctor.API.DTOs;

namespace MyDocAppointment.API.Validations
{
    public class CreateDoctorValidator : AbstractValidator<CreateDoctorDto>
    {
        public const string EMPTY_SPECIALITY_ERROR = "The Speciality cannot be empty";

        public CreateDoctorValidator()
        {
            RuleFor(d => d.SpecialityId).NotEmpty().WithMessage(EMPTY_SPECIALITY_ERROR);
            RuleFor(d => d.UserDetails).SetValidator(new UserValidator());
        }
    }

    public class UpdateDoctorPhotosValidator : AbstractValidator<UpdateDoctorPhotosDto>
    {
        private const string EMPTY_PROFILE_PHOTO_ERROR = "Profile Photo cannot be empty.";
        private const string EMPTY_DIPLOMA_PHOTO_ERROR = "Diploma Photo cannot be empty.";

        public UpdateDoctorPhotosValidator()
        {
            RuleFor(d => d.ProfilePhoto).NotEmpty().WithMessage(EMPTY_PROFILE_PHOTO_ERROR);
            RuleFor(d => d.DiplomaPhoto).NotEmpty().WithMessage(EMPTY_DIPLOMA_PHOTO_ERROR);
        }
    }
}
