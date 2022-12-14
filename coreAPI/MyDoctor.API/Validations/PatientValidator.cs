using FluentValidation;
using MyDoctor.API.DTOs;

namespace MyDocAppointment.API.Validations
{
    public class PatientValidator : AbstractValidator<CreatePatientDto>
    {
        public const string TOO_BIG_AGE_ERROR = "The age cannot exceed 120";

        public PatientValidator()
        {
            RuleFor(p => p.Age).NotEmpty().Must(IsAgeValid).WithMessage(TOO_BIG_AGE_ERROR);
            RuleFor(p => p.UserDetails).SetValidator(new UserValidator());
        }

        public bool IsAgeValid(uint age) 
        {
            if (age > 120)
                return false;
            return true;
        }
    }
}
