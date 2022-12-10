using MyDoctorApp.Domain.Helpers;

namespace MyDoctorApp.Domain.Models
{
    public class MedicalHistory
    {
        public MedicalHistory()
        {
            Id = Guid.NewGuid();
        }
        public Guid Id { get; private set; }
        public virtual Patient Patient { get; private set; }
        public Guid PatientId { get; private set; }

        public List<Prescription> Prescriptions { get; private set; } = new List<Prescription>();

        public Result RegisterPrescriptions(List<Prescription> prescriptions)
        {
            if (!prescriptions.Any())
            {
                return Result.Failure("Add at least one prescription for the current MedicalHistory");
            }


            foreach (Prescription prescription in prescriptions)
            {
                Prescriptions.Add(prescription);
            }

            return Result.Success();
        }
        public void AttachToPatient(Patient patient)
        {
            PatientId = patient.Id;
            Patient = patient;
        }
    }
}
