using FluentValidation;
using MyDoctor.Application.Queries.ScheduleIntervalQueries;

namespace MyDoctor.Application.Validators.ScheduleIntervalValidators
{
    public class GetDoctorScheduleIntervalQueryValidator : AbstractValidator<GetDoctorScheduleIntervalsQuery>
    {
        public GetDoctorScheduleIntervalQueryValidator()
        {
            RuleFor(e => e.DoctorId).NotEmpty();
        }
    }
}
