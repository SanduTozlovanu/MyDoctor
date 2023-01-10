using System.Runtime.InteropServices;

namespace MyDoctor.API.DTOs
{
    public class CreatePrescriptionDto
    {
        public CreatePrescriptionDto(string description, string name, [Optional, DefaultParameterValue(null)] List<GetDrugDto>? drugs, [Optional, DefaultParameterValue(null)] List<CreateProcedureDto>? procedures)
        {
            this.Description = description;
            this.Name = name;
            if (drugs != null)
                this.Drugs = drugs;
            if (procedures != null)
                this.Procedures = procedures;
        }

        public string Description { get; set; }
        public string Name { get; set; }
        public List<GetDrugDto>? Drugs { get; set; }
        public List<CreateProcedureDto>? Procedures { get; set; }

    }
    public class DisplayPrescriptionDto
    {
        public DisplayPrescriptionDto(Guid id, Guid appointmentId,
            string description, string name, List<DisplayPrescriptedDrugDto>? prescriptedDrugs, List<DisplayProcedureDto>? procedures)
        {
            Id = id;
            AppointmentId = appointmentId;
            Description = description;
            Name = name;
            PrescriptedDrugs = prescriptedDrugs;
            Procedures = procedures;
        }

        public Guid Id { get; set; }
        public Guid AppointmentId { get; set; }
        public string Description { get; set; }
        public string Name { get; set; }
        public List<DisplayPrescriptedDrugDto>? PrescriptedDrugs { get; set; }
        public List<DisplayProcedureDto>? Procedures { get; set; }

    }
}
