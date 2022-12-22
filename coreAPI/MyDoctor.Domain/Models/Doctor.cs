using MyDoctorApp.Domain.Helpers;

namespace MyDoctorApp.Domain.Models
{
    public class Doctor : User
    {
        public Doctor(string email, string password, string firstName, string lastName, string description="", string username="") :
            base(AccountTypes.Doctor, email, password, firstName, lastName, description, username)
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

        public static List<Tuple<TimeOnly, TimeOnly>> GetAvailableAppointmentIntervals(
            DateOnly date,
            List<ScheduleInterval> scheduleIntervals,
            List<AppointmentInterval> appointmentIntervals)
        {
            var weekDay = date.ToString("dddd");
            var appointmentDurationInMins = 30;
            List<Tuple<TimeOnly, TimeOnly>> availableIntervals = new List<Tuple<TimeOnly, TimeOnly>>();
            foreach (var interval in scheduleIntervals)
            {
                if (interval.DayOfWeek.ToString() == weekDay)
                {
                    TimeOnly intervalEndTime = interval.StartTime;

                    while ((interval.EndTime - intervalEndTime).TotalMinutes >= appointmentDurationInMins)
                    {
                        ProcessInterval(ref intervalEndTime, ref appointmentDurationInMins, ref appointmentIntervals, ref availableIntervals);
                    }
                    break;
                }
            }
            return availableIntervals;
        }
        public static void ProcessInterval(ref TimeOnly intervalEndTime,
            ref int appointmentDurationInMins, ref List<AppointmentInterval> appointmentIntervals,
            ref List<Tuple<TimeOnly, TimeOnly>> availableIntervals)
        {
            TimeOnly intervalStartTime = intervalEndTime;
            intervalEndTime = intervalStartTime.AddMinutes(appointmentDurationInMins);
            bool skipCurrentInterval = false;
            foreach (var appointmentInterval in appointmentIntervals)
            {
                if (appointmentInterval.StartTime.IsBetween(intervalStartTime, intervalEndTime) ||
                    appointmentInterval.EndTime.IsBetween(intervalStartTime, intervalEndTime) ||
                    appointmentInterval.StartTime == intervalStartTime ||
                    appointmentInterval.EndTime == intervalEndTime)
                {
                    skipCurrentInterval = true;
                    break;
                }
            }
            if (!skipCurrentInterval)
            {
                availableIntervals.Add(Tuple.Create(intervalStartTime, intervalEndTime));
            }
        }
    }
}
