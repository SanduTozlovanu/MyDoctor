using FluentValidation;
using MyDoctor.Application.Commands.ScheduleIntervalCommands;

namespace MyDoctor.Application.Validators.ScheduleIntervalValidators
{
    public class UpdateScheduleIntervalDtoValidator : AbstractValidator<UpdateScheduleIntervalDto>
    {
        private const string LATE_STARTTIME_ERROR = "Startime cannot be earlier than EndTime";

        public UpdateScheduleIntervalDtoValidator()
        {
            RuleFor(si => si.StartTime).LessThan(si => si.EndTime).NotEmpty().WithMessage(LATE_STARTTIME_ERROR);
            RuleFor(si => si.EndTime).NotEmpty();
            RuleFor(si => si.Id).NotEmpty();
        }
    }
}
