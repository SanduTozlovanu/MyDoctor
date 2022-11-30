using MyDoctor.Domain.Models;

namespace MyDoctor
{
    public class Interval
    {
        public Interval(DateOnly date, TimeOnly startTime, TimeOnly endTime)
        {
            this.Id = Guid.NewGuid();
            this.Date = date;
            this.StartTime = startTime;
            this.EndTime = endTime;
        }

        public Guid Id { get; private set; }
        public DateOnly Date { get; private set; }
        public TimeOnly StartTime { get; private set; }
        public TimeOnly EndTime { get; private set; }
    }
}