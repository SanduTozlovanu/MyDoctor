using MediatR;
using MyDoctor.Application.Mappers.ScheduleIntervalMappers;
using MyDoctor.Application.Response;
using MyDoctorApp.Domain.Models;
using MyDoctorApp.Infrastructure.Generics;

namespace MyDoctor.Application.Queries.ScheduleIntervalQueries
{
    public class GetDoctorScheduleIntervalsQueryHandler :
        IRequestHandler<GetDoctorScheduleIntervalsQuery,
            List<ScheduleIntervalResponse>>
    {
        private readonly IRepository<ScheduleInterval> repository;

        public GetDoctorScheduleIntervalsQueryHandler(IRepository<ScheduleInterval> repository)
        {
            this.repository = repository;
        }
        public async Task<List<ScheduleIntervalResponse>> Handle(GetDoctorScheduleIntervalsQuery request, CancellationToken cancellationToken)
        {
            var scheduleIntervals = (await repository.FindAsync(si => si.DoctorId == request.DoctorId)).ToList();
            return ScheduleIntervalMapper.Mapper.Map<List<ScheduleIntervalResponse>>(scheduleIntervals);
        }
    }
}
