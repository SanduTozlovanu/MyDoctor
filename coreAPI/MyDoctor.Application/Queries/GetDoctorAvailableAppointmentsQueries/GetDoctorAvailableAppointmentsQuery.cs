using MediatR;
using MyDoctor.Application.Responses;

namespace MyDoctor.Application.Queries.GetDoctorAvailableAppointmentsQueries
{
    public class GetDoctorAvailableAppointmentsQuery : IRequest<List<IntervalResponse>>
    {
        public GetDoctorAvailableAppointmentsQuery(Guid doctorId, DateOnly date)
        {
            DoctorId = doctorId;
            Date = date;
        }

        public Guid DoctorId { get; private set; }
        public DateOnly Date { get; }
    }
}
