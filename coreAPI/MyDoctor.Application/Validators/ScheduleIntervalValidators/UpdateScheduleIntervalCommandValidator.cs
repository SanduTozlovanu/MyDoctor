using FluentValidation;
using MyDoctor.Application.Commands.ScheduleIntervalCommands;

namespace MyDoctor.Application.Validators.ScheduleIntervalValidators
{
    public class UpdateMedicalRoomCommandValidator : AbstractValidator<UpdateScheduleIntervalCommand>
    {
        public UpdateMedicalRoomCommandValidator()
        {
            RuleForEach(si => si.ScheduleIntervalList).SetValidator(new UpdateScheduleIntervalDtoValidator());
        }
    }
}
