using MediatR;
using MyDoctor.Application.Response;

namespace MyDoctor.Application.Queries.ScheduleIntervalQueries
{
    public class GetDoctorScheduleIntervalsQuery : IRequest<List<ScheduleIntervalResponse>>
    {
        public GetDoctorScheduleIntervalsQuery(Guid doctorId)
        {
            DoctorId = doctorId;
        }

        public Guid DoctorId { get; private set; }
    }
}
