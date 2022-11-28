namespace MyDoctor.Domain.Models
{
    public class HospitalAdmissionFile
    {
        public HospitalAdmissionFile(string name, string description) 
        {
            this.Id = Guid.NewGuid();
            this.Name = name;
            this.Description = description;
        }
        public Guid Id { get; private set; }
        public Prescription Prescription { get; private set; }
        public Guid PrescriptionId { get; private set; }
        public string Name { get; private set; }
        public string Description { get; private set; }
        public virtual Hospital Hospital { get; private set; }
        public Guid HospitalId { get; private set; }

        public void AttachToPrescription(Prescription prescription)
        {
            this.PrescriptionId = prescription.Id;
            this.Prescription = prescription;
        }
        public void AttachToHospital(Hospital hospital) 
        {
            this.HospitalId = hospital.Id;
            this.Hospital = hospital;
        }

    }
}
