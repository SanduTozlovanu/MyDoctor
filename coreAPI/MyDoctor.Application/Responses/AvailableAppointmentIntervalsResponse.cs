namespace MyDoctor.Application.Response
{
    public class AvailableAppointmentIntervalsResponse
    {
        public AvailableAppointmentIntervalsResponse(TimeOnly startTime, TimeOnly endTime)
        {
            StartTime = startTime.ToString("HH:mm");
            EndTime = endTime.ToString("HH:mm");
        }
        public string StartTime { get; private set; }
        public string EndTime { get; private set; }

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
