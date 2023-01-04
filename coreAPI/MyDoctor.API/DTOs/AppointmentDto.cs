namespace MyDoctor.API.DTOs
{
    public class CreateAppointmentDto
    {
        public CreateAppointmentDto(double price, DateOnly date, string startTime, string endTime)
        {
            Price = price;
            Date = date;
            StartTime = startTime;
            EndTime = endTime;
        }

        public double Price { get; set; }
        public DateOnly Date { get; set; }
        public string StartTime { get; set; }
        public string EndTime { get; set; }
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
