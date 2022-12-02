namespace MyDoctor.API.DTOs
{
    public class CreatePrescriptionDto
    {
        public string Description { get; set; }
        public string Name { get; set; }
        public List<GetDrugDto> drugs { get; set; } = new List<GetDrugDto>();
        public List<CreateProcedureDto> procedures { get; set; } = new List<CreateProcedureDto>();

    }
    public class DisplayPrescriptionDto
    {
        public DisplayPrescriptionDto(Guid id, Guid appointmentId, string description, string name)
        {
            Id = id;
            AppointmentId = appointmentId;
            Description = description;
            Name = name;
        }

        public Guid Id { get; set; }
        public Guid AppointmentId { get; set; }
        public string Description { get; set; }
        public string Name { get; set; }

    }
}
