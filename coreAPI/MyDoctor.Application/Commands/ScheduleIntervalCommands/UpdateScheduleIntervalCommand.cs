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

        private List<UpdateScheduleIntervalDto> ScheduleIntervalList;
        public List<UpdateScheduleIntervalDto> GetScheduleIntervalList()
        {
            return this.ScheduleIntervalList;
        }
    }
    public class UpdateScheduleIntervalDto
    {
        public UpdateScheduleIntervalDto(Guid id, TimeOnly startTime, TimeOnly endTime)
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
