using FluentValidation;
using MyDoctor.API.DTOs;

namespace MyDocAppointment.API.Validations
{
    public class AppointmentValidator : AbstractValidator<CreateAppointmentDto>
    {
        public const string TOO_EARLY_DATE_ERROR = "The appointment date should be later than now.";
        public const string ENDTIME_EARLIER_THAN_STARTTIME_ERROR = "The appointment should end later than it starts.";
        public const string TOO_EARLY_STARTIME_ERROR = "The appointment should start later than now.";
        public const string NEGATIVE_PRICE_ERROR = "Price should not be negative.";

        public AppointmentValidator()
        {
            RuleFor(a => a.Date).NotEmpty().GreaterThan(p => DateOnly.FromDateTime(DateTime.Now)).WithMessage(TOO_EARLY_DATE_ERROR);
            RuleFor(a => a.EndTime).NotEmpty().GreaterThan(p => p.StartTime).WithMessage(ENDTIME_EARLIER_THAN_STARTTIME_ERROR);
            RuleFor(a => TimeOnly.Parse(a.StartTime)).NotEmpty().GreaterThan(p => TimeOnly.FromDateTime(DateTime.Now)).WithMessage(TOO_EARLY_STARTIME_ERROR);
            RuleFor(a => a.Price).NotEmpty().GreaterThanOrEqualTo(0).WithMessage(NEGATIVE_PRICE_ERROR);
        }
    }
}
