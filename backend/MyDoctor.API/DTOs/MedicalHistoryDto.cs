namespace MyDoctor.API.DTOs
{
    public class DisplayMedicalHistoryDto
    {
        public DisplayMedicalHistoryDto(Guid id, Guid patientId)
        { 
            this.Id = id;
            this.PatientId = patientId;
        }
        public Guid Id { get; set; }
        public Guid PatientId { get; set; }

    }
}
