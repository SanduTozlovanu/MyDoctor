using FluentValidation;
using MyDoctor.Application.Commands.ScheduleIntervalCommands;
using MyDoctorApp.Domain.Models;

namespace MyDoctor.Application.Validators.ScheduleIntervalValidators
{
    public class UpdateScheduleIntervalDtoValidator : AbstractValidator<UpdateScheduleIntervalDto>
    {
        private const string INVALID_STARTTIME_FORMAT_ERROR = "Invalid format for StartTime";
        private const string INVALID_ENDTIME_FORMAT_ERROR = "Invalid format for EndTime";

        public UpdateScheduleIntervalDtoValidator()
        {
            RuleFor(si => si.StartTime).Must(isTimeValid).WithMessage(INVALID_STARTTIME_FORMAT_ERROR);
            RuleFor(si => si.StartTime).Must(isTimeValid).WithMessage(INVALID_ENDTIME_FORMAT_ERROR);
            RuleFor(si => si.Id).NotEmpty();
        }

        public static bool isTimeValid(string time)
        {
            return TimeOnly.TryParse(time, out _);
        }
    }
}
