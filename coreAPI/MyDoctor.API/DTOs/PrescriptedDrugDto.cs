namespace MyDoctor.API.DTOs
{
    public class DisplayPrescriptedDrugDto
    {
        public DisplayPrescriptedDrugDto(Guid id, Guid drugId, Guid prescriptionId, uint quantity)
        {
            this.Id = id;
            this.DrugId = drugId;
            this.PrescriptionId = prescriptionId;
            this.Quantity = quantity;
        }
        public Guid Id { get; set; }
        public Guid DrugId { get; set; }
        public Guid PrescriptionId { get; set; }
        public uint Quantity { get; set; }
    }
}
