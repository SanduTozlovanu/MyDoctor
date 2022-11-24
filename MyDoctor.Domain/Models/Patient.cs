using MyDoctorApp.Domain.Helpers;

namespace MyDoctor.Domain.Models
{
    public class Patient
    {
        public Patient(string mail, string password, string firstName, string lastName, uint age) 
        {
            this.Id = Guid.NewGuid();
            this.Mail = mail;
            this.Password = password;
            this.FirstName = firstName;
            this.LastName = lastName;
            this.Age = age;
        }
        private const string SEPARATOR = ", ";
        public Guid Id { get; private set; }
        public string Mail { get; private set; }
        public string Password { get; private set; }
        public string FirstName { get; private set; }
        public string LastName { get; private set; }
        public uint Age { get; private set; }
        public List<Appointment> Appointments { get; private set; } = new List<Appointment>();
        public MedicalHistory MedicalHistory { get; private set; }
        public string GetFullName()
        {
            return FirstName + SEPARATOR + LastName;
        }

        public void RegisterMedicalHistory(MedicalHistory medicalHistory) 
        {
            medicalHistory.AttachToPatient(this);
            this.MedicalHistory = medicalHistory;
        }
        public Result RegisterAppointment(List<Appointment> appointments)
        {
            if (!appointments.Any())
            {
                return Result.Failure("Add at least one appointment to the current Doctor");
            }

            foreach (Appointment appointment in appointments)
            {
                appointment.AttachToPatient(this);
                this.Appointments.Add(appointment);
            }

            return Result.Success();
        }

    }
}
