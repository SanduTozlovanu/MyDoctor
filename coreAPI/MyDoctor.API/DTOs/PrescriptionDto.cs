namespace MyDoctor.API.DTOs
{
    public class CreatePrescriptionDto
    {
        public CreatePrescriptionDto(string description, string name, List<GetDrugDto> drugs, List<CreateProcedureDto> procedures)
        {
            this.Description = description;
            this.Name = name;
            this.Drugs = drugs;
            this.Procedures = procedures;
        }

        public string Description { get; set; }
        public string Name { get; set; }
        public List<GetDrugDto> Drugs { get; set; }
        public List<CreateProcedureDto> Procedures { get; set; }

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
