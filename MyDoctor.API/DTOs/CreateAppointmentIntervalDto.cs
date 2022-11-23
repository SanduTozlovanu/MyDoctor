namespace MyDoctor.API.DTOs
{
    public class CreateAppointmentIntervalDto
    {
        public DateOnly Date { get; set; }
        public TimeOnly StartTime { get; set; }
        public TimeOnly EndTime { get; set; }
    }
}
