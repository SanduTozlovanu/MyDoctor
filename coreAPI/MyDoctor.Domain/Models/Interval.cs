namespace MyDoctorApp.Domain.Models
{
    public class Interval
    {
        public Interval(DateOnly date, TimeOnly startTime, TimeOnly endTime)
        {
            Id = Guid.NewGuid();
            Date = date;
            StartTime = startTime;
            EndTime = endTime;
        }

        public Guid Id { get; private set; }
        public DateOnly Date { get; private set; }
        public TimeOnly StartTime { get; private set; }
        public TimeOnly EndTime { get; private set; }
    }
}