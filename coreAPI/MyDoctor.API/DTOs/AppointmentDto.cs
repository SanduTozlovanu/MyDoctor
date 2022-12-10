namespace MyDoctor.API.DTOs
{
    public class CreateAppointmentDto
    {
        public CreateAppointmentDto(double price, DateTime date, DateTime startTime, DateTime endTime)
        {
            Price = price;
            Date = date;
            StartTime = startTime;
            EndTime = endTime;
        }

        public double Price { get; set; }
        public DateTime Date { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
    }
    public class DisplayAppointmentDto
    {
        public DisplayAppointmentDto(Guid id, Guid patientId, Guid doctorId, double price)
        {
            this.Id = id;
            this.PatientId = patientId;
            this.DoctorId = doctorId;
            this.Price = price;
        }
        public Guid Id { get; set; }
        public Guid PatientId { get; set; }
        public Guid DoctorId { get; set; }
        public double Price { get; set; }
    }
}
