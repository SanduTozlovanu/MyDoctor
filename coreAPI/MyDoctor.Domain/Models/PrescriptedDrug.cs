namespace MyDoctorApp.Domain.Models
{
    public class PrescriptedDrug
    {
        public PrescriptedDrug(Prescription prescription, Drug drug, uint quantity)
        {
            Id = Guid.NewGuid();
            prescription.RegisterPrescriptedDrugs(new List<PrescriptedDrug> { this });
            AttachDrug(drug);
            Quantity = quantity;
        }

        public Guid Id { get; private set; }
        public virtual Prescription Prescription { get; private set; }
        public Guid PrescriptionId { get; private set; }
        public virtual Drug Drug { get; private set; }
        public Guid DrugId { get; private set; }
        public uint Quantity { get; private set; }

        public void AttachPrescription(Prescription prescription)
        {
            Prescription = prescription;
            PrescriptionId = prescription.Id;
        }
        public void AttachDrug(Drug drug)
        {
            Drug = drug;
            DrugId = drug.Id;
        }

    }
}
