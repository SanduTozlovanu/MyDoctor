using MyDoctorApp.Domain.Helpers;

namespace MyDoctorApp.Domain.Models
{
    public class Patient : User
    {
        public Patient(string email, string password, string firstName, string lastName, uint age) : 
            base(AccountTypes.Patient, email, password, firstName, lastName)
        {
            Age = age;
            Appointments = new List<Appointment>();
        }
        public uint Age { get; private set; }
        public virtual List<Appointment> Appointments { get; private set; }
        public virtual MedicalHistory MedicalHistory { get; private set; }
        public void RegisterMedicalHistory(MedicalHistory medicalHistory)
        {
            medicalHistory.AttachToPatient(this);
            MedicalHistory = medicalHistory;
        }
        public void RegisterAppointment(Appointment appointment)
        {
            appointment.AttachToPatient(this);
            Appointments.Add(appointment);
        }

        public void Update(Patient patient)
        {
            base.Update(patient);
            Age = patient.Age;
        }

    }
}
