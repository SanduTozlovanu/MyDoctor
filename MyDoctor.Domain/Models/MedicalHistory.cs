using MyDoctorApp.Domain.Helpers;

namespace MyDoctor.Domain.Models
{
    public class MedicalHistory
    {
        public MedicalHistory()
        {
            this.Id = Guid.NewGuid();
        }
        public Guid Id { get; private set; }
        public Patient Patient { get; private set; }
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
                this.Prescriptions.Add(prescription);
            }

            return Result.Success();
        }
        public void AttachPatient(Patient patient)
        {
            this.PatientId = patient.Id;
            this.Patient = patient;
        }
    }
}
