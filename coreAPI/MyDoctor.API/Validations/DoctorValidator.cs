using FluentValidation;
using MyDoctor.API.DTOs;

namespace MyDocAppointment.API.Validations
{
    public class DoctorValidator : AbstractValidator<CreateDoctorDto>
    {
        public const string EMPTY_SPECIALITY_ERROR = "The Speciality cannot be empty";

        public DoctorValidator()
        {
            RuleFor(d => d.SpecialityId).NotEmpty().WithMessage(EMPTY_SPECIALITY_ERROR);
            RuleFor(d => d.UserDetails).SetValidator(new UserValidator());
        }
    }
}
