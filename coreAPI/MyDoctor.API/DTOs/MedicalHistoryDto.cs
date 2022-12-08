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

        public override bool Equals(object? obj)
        {
            return obj is DisplayMedicalHistoryDto dto &&
                   Id.Equals(dto.Id) &&
                   PatientId.Equals(dto.PatientId);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Id, PatientId);
        }
    }
}
