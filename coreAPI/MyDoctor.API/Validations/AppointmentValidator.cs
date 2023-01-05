using FluentValidation;
using MyDoctor.API.DTOs;

namespace MyDocAppointment.API.Validations
{
    public class AppointmentValidator : AbstractValidator<CreateAppointmentDto>
    {
        public const string TOO_EARLY_DATE_ERROR = "The appointment date should be today or later than today.";
        public const string TOO_EARLY_STARTIME_ERROR = "The appointment should start later than now.";

        public AppointmentValidator()
        {
            RuleFor(a => a.Date).NotEmpty().GreaterThan(p => DateOnly.FromDateTime(DateTime.Now)).WithMessage(TOO_EARLY_DATE_ERROR);
            RuleFor(a => a.Date.ToDateTime(TimeOnly.Parse(a.StartTime))).NotEmpty().GreaterThan(p => DateTime.Now).WithMessage(TOO_EARLY_STARTIME_ERROR);
        }
    }
}
