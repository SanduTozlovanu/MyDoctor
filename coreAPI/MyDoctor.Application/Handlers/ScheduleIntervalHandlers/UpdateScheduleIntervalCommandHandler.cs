using MediatR;
using Microsoft.AspNetCore.Mvc;
using MyDoctor.Application.Commands.ScheduleIntervalCommands;
using MyDoctor.Application.Mappers.ScheduleIntervalMappers;
using MyDoctor.Application.Responses;
using MyDoctorApp.Domain.Models;
using MyDoctorApp.Infrastructure.Generics;

namespace MyDoctor.Application.Handlers.ScheduleIntervalHandlers
{
    public class UpdateScheduleIntervalCommandHandler : IRequestHandler<UpdateScheduleIntervalCommand, List<ScheduleIntervalResponse>>
    {
        private const string SCHEDULE_INTERVAL_NOTFOUND_ERROR = "Could not find an interval with this schedule id";
        private readonly IRepository<ScheduleInterval> repository;

        public UpdateScheduleIntervalCommandHandler(IRepository<ScheduleInterval> repository)
        {
            this.repository = repository;
        }
        public async Task<List<ScheduleIntervalResponse>> Handle(UpdateScheduleIntervalCommand request, CancellationToken cancellationToken)
        {
            List<ScheduleInterval> scheduleIntervals = new();
            bool errorFound = false;
            request.ScheduleIntervalList.ForEach(async interval =>
            {
                ScheduleInterval? scheduleIntervalEntity = await repository.GetAsync(interval.Id);
                TimeOnly startTime = TimeOnly.Parse(interval.StartTime);
                TimeOnly endTime = TimeOnly.Parse(interval.EndTime);

                if (scheduleIntervalEntity == null)
                {
                    errorFound = true;
                    return;
                }
                scheduleIntervalEntity.Update(startTime, endTime);

                scheduleIntervals.Add(repository.Update(scheduleIntervalEntity));
            });
            if (errorFound)
            {
                var responseList = new List<ScheduleIntervalResponse>();
                var intervalResponse = new ScheduleIntervalResponse(Guid.Empty, string.Empty, string.Empty, string.Empty, Guid.Empty);
                intervalResponse.SetStatusResult(new NotFoundObjectResult(SCHEDULE_INTERVAL_NOTFOUND_ERROR));
                responseList.Add(intervalResponse);
                return responseList;
            }

            await repository.SaveChangesAsync();
            return ScheduleIntervalMapper.Mapper.Map<List<ScheduleIntervalResponse>>(scheduleIntervals);
        }
    }
}
