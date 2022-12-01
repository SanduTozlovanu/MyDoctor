using MyDoctorApp.Domain.Helpers;

namespace MyDoctor.Domain.Models
{
    public class Patient: User
    {
        public Patient(string email, string password, string firstName, string lastName, uint age):
            base(AccountTypes.Patient, email, password, firstName, lastName)
        {
            this.Age = age;
        }
        public uint Age { get; private set; }
        public List<Appointment> Appointments { get; private set; } = new List<Appointment>();
        public MedicalHistory MedicalHistory { get; private set; }
        public void RegisterMedicalHistory(MedicalHistory medicalHistory) 
        {
            medicalHistory.AttachToPatient(this);
            this.MedicalHistory = medicalHistory;
        }
        public void RegisterAppointment(Appointment appointment)
        {
            appointment.AttachToPatient(this);
            this.Appointments.Add(appointment);
        }

    }
}
