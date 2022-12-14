using MyDoctorApp.Domain.Helpers;

namespace MyDoctorApp.Domain.Models
{
    public class Doctor : User
    {
        public Doctor(string email, string password, string firstName, string lastName, Speciality speciality, MedicalRoom medicalRoom, List<ScheduleInterval> scheduleIntervals) :
            base(AccountTypes.Doctor, email, password, firstName, lastName)
        {
            speciality.RegisterDoctor(this);
            medicalRoom.RegisterDoctors(new List<Doctor> { this });
            Appointments = new List<Appointment>();
            ScheduleIntervals = scheduleIntervals;
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

        public void Update(Doctor doctor)
        {
            base.Update(doctor);
            Speciality = doctor.Speciality;
        }
    }
}
