using MyDoctorApp.Domain.Helpers;

namespace MyDoctor.Domain.Models
{
    public class Doctor
    {
        public Doctor(string mail, string password, string firstName, string lastName, string speciality)
        {
            this.Id = Guid.NewGuid();
            this.Mail = mail;
            this.Password = password;
            this.FirstName = firstName;
            this.Password = password;   
            this.LastName = lastName;
            this.Speciality = speciality;
        }
        private const string SEPARATOR = ", ";
        public Guid Id { get; private set; }
        public Guid MedicalRoomId { get; private set; }
        public string Mail { get; private set; }
        public string Password { get; private set; }
        public string Speciality { get; private set; }
        public string FirstName { get; private set; }
        public string LastName { get; private set; }
        public List<Appointment> Appointments { get; private set; }
        public List<AppointmentInterval> AppointmentIntervals { get; private set; } = new List<AppointmentInterval>();

        public string GetFullName()
        {
            return FullName;
        }
        public string FullName
        {
            get
            {
                return FirstName + SEPARATOR + LastName;
            }
        }
        public void AttachMedicalRoom(MedicalRoom medicalRoom)
        {
            this.MedicalRoomId = medicalRoom.Id;
        }

        public Result RegisterAppointment(List<Appointment> appointments)
        {
            if (!appointments.Any())
            {
                return Result.Failure("Add at least one appointment to the current Doctor");
            }

            foreach (Appointment appointment in appointments)
            {
                appointment.AttachDoctor(this);
                this.Appointments.Add(appointment);
            }

            return Result.Success();
        }
        public Result RegisterAppointmentIntervals(List<AppointmentInterval> appointmentIntervals)
        {
            if (!appointmentIntervals.Any())
            {
                return Result.Failure("Add at least one appointment to the current Doctor");
            }

            foreach (AppointmentInterval appointmentInterval in appointmentIntervals)
            {
                appointmentInterval.AttachDoctor(this);
                this.AppointmentIntervals.Add(appointmentInterval);
            }

            return Result.Success();
        }
    }
}
