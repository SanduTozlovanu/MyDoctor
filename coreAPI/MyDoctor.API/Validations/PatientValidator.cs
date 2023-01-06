using FluentValidation;
using MyDoctor.API.DTOs;

namespace MyDoctor.API.Validations
{
    public class PatientValidator : AbstractValidator<CreatePatientDto>
    {
        public PatientValidator()
        {
            RuleFor(p => p.UserDetails).SetValidator(new UserValidator());
        }
    }
}
