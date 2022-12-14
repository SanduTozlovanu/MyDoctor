namespace MyDoctorApp.Domain.Models
{
    public class Procedure
    {
        public Procedure(string name, string description, double price)
        {
            Id = Guid.NewGuid();
            Name = name;
            Description = description;
            Price = price;
        }
        public Guid Id { get; private set; }
        public virtual Prescription Prescription { get; private set; }
        public Guid PrescriptionId { get; private set; }
        public string Name { get; private set; }
        public string Description { get; private set; }
        public double Price { get; private set; }

        public void AttachToPrescription(Prescription prescription)
        {
            PrescriptionId = prescription.Id;
            Prescription = prescription;
        }

    }
}
