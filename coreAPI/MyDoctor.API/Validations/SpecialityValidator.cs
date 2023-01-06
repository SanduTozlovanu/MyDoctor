using FluentValidation;
using MyDoctor.API.DTOs;

namespace MyDoctor.API.Validations
{
    public class SpecialityValidator : AbstractValidator<CreateSpecialityDto>
    {
        public SpecialityValidator()
        {
            RuleFor(s => s.Name).NotEmpty();
        }
    }
}
