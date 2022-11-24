using MyDoctorApp.Domain.Helpers;

namespace MyDoctor.Domain.Models
{
    public class Prescription
    {
        public Prescription(string name, string description) 
        {
            this.Id = Guid.NewGuid();
            this.Description= description;
            this.Name= name;
        }
        public Guid Id { get; private set; }
        public string Name { get; private set; }
        public string Description { get; private set; }
        public List<Drug>? Drugs { get; private set; } = new List<Drug>();
        public List<Procedure>? Procedures { get; private set; } = new List<Procedure>();
        public HospitalAdmissionFile HospitalAdmissionFile { get; private set; }
        public Appointment Appointment { get; private set; }
        public Guid AppointmentId { get; private set; }
        public void AttachAppointment(Appointment appointment)
        {
            this.AppointmentId = appointment.Id;
            this.Appointment = appointment;
        }
        public Result RegisterProcedures(List<Procedure> procedures) 
        {
            if (!procedures.Any())
            {
                return Result.Failure("Add at least one procedure for the current Prescription");
            }


            foreach (Procedure procedure in procedures)
            {
                procedure.AttachPrescription(this);
                this.Procedures.Add(procedure);
            }

            return Result.Success();
        }

        public Result RegisterDrugs(List<Drug> drugs)
        {
            if (!drugs.Any())
            {
                return Result.Failure("Add at least one drug for the current Prescription");
            }


            foreach (Drug drug in drugs)
            {
                this.Drugs.Add(drug);
            }

            return Result.Success();
        }

        public void RegisterHospitalAdmissionFile(HospitalAdmissionFile hospitalAdmissionFile) 
        {
            hospitalAdmissionFile.AttachPrescription(this);
            this.HospitalAdmissionFile = hospitalAdmissionFile;
        }
    }
}
