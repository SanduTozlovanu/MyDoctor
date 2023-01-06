using FluentValidation;
using MyDoctor.API.DTOs;

namespace MyDoctor.API.Validations
{
    public class PrescriptionValidator : AbstractValidator<CreatePrescriptionDto>
    {
        public const string WRONG_NAME_LENGTH_ERROR = "The name should be between 5 and 100 characters";
        public const string DESCRIPTION_LENGTH_ERROR = "The description should be between 10 and 400 characters";
        public PrescriptionValidator()
        {
            RuleFor(p => p.Name).NotEmpty().Length(5, 100).WithMessage(WRONG_NAME_LENGTH_ERROR);
            RuleFor(p => p.Description).NotEmpty().Length(10, 400).WithMessage(DESCRIPTION_LENGTH_ERROR);
            RuleForEach(p => p.Procedures).SetValidator(new ProcedureValidator());
            RuleForEach(p => p.Drugs).SetValidator(new GetDrugValidator());
        }
    }
}
