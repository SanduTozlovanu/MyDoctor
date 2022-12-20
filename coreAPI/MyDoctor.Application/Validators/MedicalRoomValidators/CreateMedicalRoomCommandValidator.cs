using FluentValidation;
using MyDoctor.Application.Commands.MedicalRoomCommands;

namespace MyDoctor.Application.Validators.MedicalRoomValidators
{
    public class CreateMedicalRoomCommandValidator : AbstractValidator<CreateMedicalRoomCommand>
    {
        public const string WRONG_ADDRESS_LENGTH = "The address length should be between 5 and 200";
        public CreateMedicalRoomCommandValidator()
        {
            RuleFor(mr => mr.Adress).NotEmpty().Length(5, 200).WithMessage(WRONG_ADDRESS_LENGTH);
        }
    }
}
