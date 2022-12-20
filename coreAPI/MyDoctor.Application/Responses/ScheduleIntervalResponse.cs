namespace MyDoctor.Application.Response
{
    public class ScheduleIntervalResponse
    {
       public ScheduleIntervalResponse(Guid id, TimeOnly startTime, TimeOnly endTime, string dayOfWeek, Guid doctorId)
        {
            Id = id;
            StartTime = startTime;
            EndTime = endTime;
            DayOfWeek = dayOfWeek;
            DoctorId = doctorId;
        }
        public Guid Id { get; private set; }
        public TimeOnly StartTime { get; private set; }
        public TimeOnly EndTime { get; private set; }
        public string DayOfWeek { get; set; }
        public Guid DoctorId { get; private set; }

        public override bool Equals(object? obj)
        {
            return obj is ScheduleIntervalResponse response &&
                   Id.Equals(response.Id) &&
                   StartTime.Equals(response.StartTime) &&
                   EndTime.Equals(response.EndTime) &&
                   DayOfWeek == response.DayOfWeek &&
                   DoctorId.Equals(response.DoctorId);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Id, StartTime, EndTime, DayOfWeek, DoctorId);
        }
    }
}
