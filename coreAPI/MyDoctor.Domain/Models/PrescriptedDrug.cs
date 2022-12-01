namespace MyDoctor.Domain.Models
{
    public class PrescriptedDrug
    {
        public PrescriptedDrug(uint quantity)
        {
            this.Id = Guid.NewGuid();
            this.Quantity = quantity;
        }

        public Guid Id { get; private set; }
        public Prescription Prescription { get; private set; }
        public Guid PrescriptionId { get; private set; }
        public Guid DrugId { get; private set; }
        public Drug Drug { get; private set; }
        public uint Quantity { get; private set; }

        public void AttachPrescription(Prescription prescription)
        {
            this.Prescription = prescription;
            this.PrescriptionId = prescription.Id;
        }
        public void AttachDrug(Drug drug) 
        {
            this.Drug = drug;
            this.DrugId = drug.Id;
        }
        
    }
}
