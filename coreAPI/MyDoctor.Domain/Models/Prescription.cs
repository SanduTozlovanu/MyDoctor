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
        public List<PrescriptedDrug> PrescriptedDrugs { get; private set; } = new List<PrescriptedDrug>();
        public List<Procedure> Procedures { get; private set; } = new List<Procedure>();
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
                procedure.AttachToPrescription(this);
                this.Procedures.Add(procedure);
            }

            return Result.Success();
        }

        public Result RegisterPrescriptedDrugs(List<PrescriptedDrug> prescriptedDrugs)
        {
            if (!prescriptedDrugs.Any())
            {
                return Result.Failure("Add at least one drug for the current Prescription");
            }


            foreach (PrescriptedDrug drug in prescriptedDrugs)
            {
                this.PrescriptedDrugs.Add(drug);
                drug.AttachPrescription(this);
            }

            return Result.Success();
        }
    }
}
