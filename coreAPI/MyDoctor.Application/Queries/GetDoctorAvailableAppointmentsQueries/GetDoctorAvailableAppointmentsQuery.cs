using MediatR;
using MyDoctor.Application.Response;

namespace MyDoctor.Application.Queries.GetDoctorAvailableAppointmentsQueries
{
    public class GetDoctorAvailableAppointmentsQuery : IRequest<List<IntervalResponse>>
    {
        public GetDoctorAvailableAppointmentsQuery(Guid doctorId, DateOnly dateOnly)
        {
            DoctorId = doctorId;
            DateOnly = dateOnly;
        }

        public Guid DoctorId { get; private set; }
        public DateOnly DateOnly { get; }
    }
}
