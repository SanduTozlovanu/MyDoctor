namespace MyDoctor.API.DTOs
{
    public class CreateAppointmentDto
    {
        public CreateAppointmentDto(DateOnly date, string startTime, string endTime)
        {
            Date = date;
            StartTime = startTime;
            EndTime = endTime;
        }

        public DateOnly Date { get; set; }
        public string StartTime { get; set; }
        public string EndTime { get; set; }
    }
    public class DisplayAppointmentDto
    {
        public DisplayAppointmentDto(Guid id, Guid patientId, Guid doctorId)
        {
            this.Id = id;
            this.PatientId = patientId;
            this.DoctorId = doctorId;
        }
        public Guid Id { get; set; }
        public Guid PatientId { get; set; }
        public Guid DoctorId { get; set; }
    }
    public class DisplayAppointmentInformationDto
    {
        public DisplayAppointmentInformationDto(string date, string patientFirstName, string patientLastName, string email, string startTime, string endTime)
        {
            Date = date;
            PatientFirstName = patientFirstName;
            PatientLastName = patientLastName;
            Email = email;
            StartTime = startTime;
            EndTime = endTime;
        }

        public string Date { get; set; }
        public string PatientFirstName { get; set; }
        public string PatientLastName { get; set; }
        public string Email { get; set; }
        public string StartTime { get; set; }
        public string EndTime { get; set; }
    }
}
