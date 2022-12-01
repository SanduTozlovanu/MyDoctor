namespace MyDoctor.Domain.Models
{
    public class Patient
    {
        public Patient(string email, string password, string firstName, string lastName, uint age) 
        {
            this.Id = Guid.NewGuid();
            this.Email = email;
            this.Password = password;
            this.FirstName = firstName;
            this.LastName = lastName;
            this.Age = age;
        }
        private const string SEPARATOR = ", ";
        public Guid Id { get; private set; }
        public string Email { get; private set; }
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
        public void RegisterAppointment(Appointment appointment)
        {
                appointment.AttachToPatient(this);
                this.Appointments.Add(appointment);
        }

    }
}
