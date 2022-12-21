using MediatR;
using MyDoctor.Application.Response;

namespace MyDoctor.Application.Commands.ScheduleIntervalCommands
{
    public class UpdateScheduleIntervalCommand : IRequest<List<ScheduleIntervalResponse>>
    {
        public UpdateScheduleIntervalCommand(List<UpdateScheduleIntervalDto> scheduleIntervalList)
        {
            this.ScheduleIntervalList = scheduleIntervalList;
        }

        public List<UpdateScheduleIntervalDto> ScheduleIntervalList; //NOSONAR
    }
    public class UpdateScheduleIntervalDto
    {
        public UpdateScheduleIntervalDto(Guid id, string startTime, string endTime)
        {
            Id = id;
            StartTime = startTime;
            EndTime = endTime;
        }

        public Guid Id { get; private set; }
        public string StartTime { get; private set; }
        public string EndTime { get; private set; }
    }

}
