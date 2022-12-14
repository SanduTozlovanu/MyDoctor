using MyDoctorApp.Domain.Helpers;

namespace MyDoctorApp.Domain.Models
{
    public class Doctor : User
    {
        public Doctor(string email, string password, string firstName, string lastName) :
            base(AccountTypes.Doctor, email, password, firstName, lastName)
        {
            Appointments = new List<Appointment>();
            ScheduleIntervals = new List<ScheduleInterval>();
        }
        public virtual MedicalRoom MedicalRoom { get; private set; }
        public Guid MedicalRoomId { get; private set; }
        public virtual Speciality Speciality { get; private set; }
        public Guid SpecialityID { get; private set; }
        public virtual List<Appointment> Appointments { get; private set; }
        public virtual List<ScheduleInterval> ScheduleIntervals { get; private set; }

        public void AttachToMedicalRoom(MedicalRoom medicalRoom)
        {
            MedicalRoomId = medicalRoom.Id;
            MedicalRoom = medicalRoom;
        }

        public void AttachToSpeciality(Speciality speciality)
        {
            Speciality = speciality;
            SpecialityID = speciality.Id;
        }

        public void RegisterAppointment(Appointment appointment)
        {
            appointment.AttachToDoctor(this);
            Appointments.Add(appointment);
        }

        public void RegisterScheduleIntervals(List<ScheduleInterval> scheduleIntervals)
        {
            foreach (var si in scheduleIntervals)
            {
                si.AttachToDoctor(this);
                ScheduleIntervals.Add(si);
            }
        }

        public void Update(Doctor doctor)
        {
            base.Update(doctor);
            Speciality = doctor.Speciality;
        }
    }
}
