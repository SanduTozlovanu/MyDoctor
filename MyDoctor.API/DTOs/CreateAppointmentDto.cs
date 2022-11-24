namespace MyDoctor.API.DTOs
{
    public class CreateAppointmentDto
    {
        public double Price { get; set; }
        public DateTime Date { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
    }
}
