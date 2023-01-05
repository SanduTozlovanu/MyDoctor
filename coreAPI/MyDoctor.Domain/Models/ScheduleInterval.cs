namespace MyDoctorApp.Domain.Models
{
    public class ScheduleInterval : Interval
    {
        public ScheduleInterval(string dayOfWeek, TimeOnly startTime, TimeOnly endTime) : base(startTime, endTime)
        {
            DayOfWeek = dayOfWeek;
        }
        public string DayOfWeek { get; set; }
        public virtual Doctor Doctor { get; private set; }
        public Guid DoctorId { get; private set; }

        public void AttachToDoctor(Doctor doctor)
        {
            DoctorId = doctor.Id;
            Doctor = doctor;
        }

    }
}