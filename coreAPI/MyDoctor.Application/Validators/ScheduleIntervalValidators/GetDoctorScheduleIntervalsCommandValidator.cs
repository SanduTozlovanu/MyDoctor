using FluentValidation;
using MyDoctor.Application.Queries.GetDoctorAvailableAppointmentsQueries;

namespace MyDoctor.Application.Validators.ScheduleIntervalValidators
{
    public class GetDoctorScheduleIntervalQueryValidator : AbstractValidator<GetDoctorAvailableAppointmentsQuery>
    {
        public GetDoctorScheduleIntervalQueryValidator()
        {
            RuleFor(e => e.DoctorId).NotEmpty();
        }
    }
}
