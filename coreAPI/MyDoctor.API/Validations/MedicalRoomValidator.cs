using FluentValidation;
using MyDoctor.API.DTOs;

namespace MyDocAppointment.API.Validations
{
    public class MedicalRoomValidator : AbstractValidator<CreateMedicalRoomDto>
    {
        private const string WRONG_ADDRESS_LENGTH = "The address length should be between 5 and 200";
        public MedicalRoomValidator()
        {
            RuleFor(mr => mr.Adress).NotEmpty().Length(5, 200).WithMessage(WRONG_ADDRESS_LENGTH);
        }
    }
}
