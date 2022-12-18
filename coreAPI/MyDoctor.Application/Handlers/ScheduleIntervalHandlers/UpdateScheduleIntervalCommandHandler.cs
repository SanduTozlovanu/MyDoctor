using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;
using MyDoctor.Application.Commands.ScheduleIntervalCommands;
using MyDoctor.Application.Mappers.ScheduleIntervalMappers;
using MyDoctor.Application.Response;
using MyDoctorApp.Domain.Models;
using MyDoctorApp.Infrastructure.Generics;
using System.Data.SqlTypes;

namespace MyDoctor.Application.Handlers.ScheduleIntervalHandlers
{
    public class UpdateScheduleIntervalCommandHandler : IRequestHandler<UpdateScheduleIntervalCommand, ScheduleIntervalResponse>
    {
        private readonly IRepository<ScheduleInterval> repository;

        public UpdateScheduleIntervalCommandHandler(IRepository<ScheduleInterval> repository)
        {
            this.repository = repository;
        }
        public async Task<ScheduleIntervalResponse> Handle(UpdateScheduleIntervalCommand request, CancellationToken cancellationToken)
        {
            ScheduleInterval? scheduleIntervalEntity = await repository.GetAsync(request.Id);
            if (scheduleIntervalEntity == null) 
            {
                throw new SqlNullValueException();
            }
            scheduleIntervalEntity.Update(request.StartTime, request.EndTime);  

            var newScheduleInterval = repository.Update(scheduleIntervalEntity);
            await repository.SaveChangesAsync();
            return ScheduleIntervalMapper.Mapper.Map<ScheduleIntervalResponse>(newScheduleInterval);
        }
    }
}
