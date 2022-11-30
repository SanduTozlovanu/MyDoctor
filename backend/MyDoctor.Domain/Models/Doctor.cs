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
        public virtual MedicalRoom MedicalRoom { get; private set; }
        public Guid MedicalRoomId { get; private set; }
        public string Mail { get; private set; }
        public string Password { get; private set; }
        public string Speciality { get; private set; }
        public string FirstName { get; private set; }
        public string LastName { get; private set; }
        public List<Appointment> Appointments { get; private set; } = new List<Appointment>();
        public List<ScheduleInterval> ScheduleIntervals { get; private set; } = new List<ScheduleInterval>();

        public string GetFullName()
        {
            return FirstName + SEPARATOR + LastName;
        }

        public void AttachToMedicalRoom(MedicalRoom medicalRoom)
        {
            this.MedicalRoomId = medicalRoom.Id;
            this.MedicalRoom = medicalRoom;
        }

        public void RegisterAppointment(Appointment appointment)
        {
            appointment.AttachToDoctor(this);
            this.Appointments.Add(appointment);
        }
        //public Result RegisterAppointmentIntervals(List<AppointmentInterval> appointmentIntervals)
        //{
        //    if (!appointmentIntervals.Any())
        //    {
        //        return Result.Failure("Add at least one appointment to the current Doctor");
        //    }

        //    foreach (AppointmentInterval appointmentInterval in appointmentIntervals)
        //    {
        //        appointmentInterval.AttachDoctor(this);
        //        this.AppointmentIntervals.Add(appointmentInterval);
        //    }

        //    return Result.Success();
        //}
    }
}
