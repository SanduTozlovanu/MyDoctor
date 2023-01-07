namespace MyDoctor.Application.Responses
{
    public class IntervalResponse : BaseResponse
    {
        public IntervalResponse(string startTime, string endTime)
        {

            var auxSTime = TimeOnly.Parse(startTime);
            startTime = auxSTime.ToString("HH:mm");
            var auxETime = TimeOnly.Parse(endTime);
            endTime = auxETime.ToString("HH:mm");
            StartTime = startTime;
            EndTime = endTime;
        }
        public string StartTime { get; private set; }
        public string EndTime { get; private set; }

        public override bool Equals(object? obj)
        {
            return obj is IntervalResponse response &&
                    StartTime.Equals(response.StartTime) &&
                    EndTime.Equals(response.EndTime);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(StartTime, EndTime);
        }
    }
}
