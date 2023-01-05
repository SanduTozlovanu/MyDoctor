using FluentValidation;
using MyDoctor.Application.Commands.ScheduleIntervalCommands;

namespace MyDoctor.Application.Validators.ScheduleIntervalValidators
{
    public class UpdateScheduleIntervalDtoValidator : AbstractValidator<UpdateScheduleIntervalDto>
    {
        private const string INVALID_STARTTIME_FORMAT_ERROR = "Invalid format for StartTime";
        private const string INVALID_ENDTIME_FORMAT_ERROR = "Invalid format for EndTime";

        public UpdateScheduleIntervalDtoValidator()
        {
            RuleFor(si => si.StartTime).Must(IsTimeValid).WithMessage(INVALID_STARTTIME_FORMAT_ERROR);
            RuleFor(si => si.StartTime).Must(IsTimeValid).WithMessage(INVALID_ENDTIME_FORMAT_ERROR);
            RuleFor(si => si.Id).NotEmpty();
        }

        public static bool IsTimeValid(string time)
        {
            return TimeOnly.TryParse(time, out _);
        }
    }
}
