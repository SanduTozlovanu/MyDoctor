using MediatR;
using MyDoctor.Application.Commands.ScheduleIntervalCommands;
using MyDoctor.Application.Mappers.ScheduleIntervalMappers;
using MyDoctor.Application.Response;
using MyDoctorApp.Domain.Models;
using MyDoctorApp.Infrastructure.Generics;
using System.Data.SqlTypes;

namespace MyDoctor.Application.Handlers.ScheduleIntervalHandlers
{
    public class UpdateScheduleIntervalCommandHandler : IRequestHandler<UpdateScheduleIntervalCommand, List<ScheduleIntervalResponse>>
    {
        private readonly IRepository<ScheduleInterval> repository;

        public UpdateScheduleIntervalCommandHandler(IRepository<ScheduleInterval> repository)
        {
            this.repository = repository;
        }
        public async Task<List<ScheduleIntervalResponse>> Handle(UpdateScheduleIntervalCommand request, CancellationToken cancellationToken)
        {
            List<ScheduleInterval> scheduleIntervals = new();
            request.ScheduleIntervalList.ForEach(async interval =>
            {
                ScheduleInterval? scheduleIntervalEntity = await repository.GetAsync(interval.Id);
                try
                {
                    TimeOnly startTime = TimeOnly.Parse(interval.StartTime);
                    TimeOnly endTime = TimeOnly.Parse(interval.EndTime);

                    if (scheduleIntervalEntity == null)
                    {
                        throw new SqlNullValueException();
                    }
                    scheduleIntervalEntity.Update(startTime, endTime);

                    scheduleIntervals.Add(repository.Update(scheduleIntervalEntity));
                }
                catch (Exception ex) when (ex is ArgumentNullException ||
                               ex is FormatException)
                {
                    throw new SqlNullValueException();
                }


            });

            await repository.SaveChangesAsync();
            return ScheduleIntervalMapper.Mapper.Map<List<ScheduleIntervalResponse>>(scheduleIntervals);
        }
    }
}
