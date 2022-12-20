using MediatR;
using MyDoctor.Application.Response;
using MyDoctorApp.Domain.Models;

namespace MyDoctor.Application.Commands.ScheduleIntervalCommands
{
    public class UpdateScheduleIntervalCommand : IRequest<ScheduleIntervalResponse>
    {
        public UpdateScheduleIntervalCommand(Guid id, TimeOnly startTime, TimeOnly endTime)
        {
            Id = id;
            StartTime = startTime;
            EndTime = endTime;
        }

        public Guid Id { get; private set; }
        public TimeOnly StartTime { get; private set; }
        public TimeOnly EndTime { get; private set; }
    }

}
