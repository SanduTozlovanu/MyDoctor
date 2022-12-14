namespace MyDoctorApp.Domain.Models
{
    public class Interval
    {
        public Interval(TimeOnly startTime, TimeOnly endTime)
        {
            Id = Guid.NewGuid();
            StartTime = startTime;
            EndTime = endTime;
        }

        public Guid Id { get; private set; }
        public TimeOnly StartTime { get; private set; }
        public TimeOnly EndTime { get; private set; }
    }
}