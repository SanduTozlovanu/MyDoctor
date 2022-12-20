namespace MyDoctor.Application.Response
{
    public class AvailableAppointmentIntervalsResponse
    {
        public AvailableAppointmentIntervalsResponse(TimeOnly startTime, TimeOnly endTime)
        {
            StartTime = startTime;
            EndTime = endTime;
        }
        public TimeOnly StartTime { get; private set; }
        public TimeOnly EndTime { get; private set; }

        public override bool Equals(object? obj)
        {
            return obj is AvailableAppointmentIntervalsResponse response &&
                    StartTime.Equals(response.StartTime) &&
                    EndTime.Equals(response.EndTime);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(StartTime, EndTime);
        }
    }
}
