using FluentValidation;
using MyDoctor.Application.Commands.ScheduleIntervalCommands;
using MyDoctorApp.Domain.Helpers;

namespace MyDoctor.Application.Validators.ScheduleIntervalValidators
{
    public class UpdateScheduleIntervalCommandValidator : AbstractValidator<UpdateScheduleIntervalCommand>
    {

        public UpdateScheduleIntervalCommandValidator()
        {
            RuleForEach(si => si.ScheduleIntervalList).SetValidator(new UpdateScheduleIntervalDtoValidator());
        }
    }
}
