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
        public TimeOnly StartTime { get; protected set; }
        public TimeOnly EndTime { get; protected set; }
        public void Update(TimeOnly startTime, TimeOnly endTime)
        {
            this.StartTime = startTime;
            this.EndTime = endTime;
        }
    }
}