using MyDoctor.Domain.Models;

namespace MyDoctor
{
    public class ScheduleInterval : Interval
    {
        public ScheduleInterval(DateOnly date, TimeOnly startTime, TimeOnly endTime) : base(date, startTime, endTime) {}
        public Doctor Doctor { get; private set; }
        public Guid DoctorId { get; private set; }

        public void AttachToDoctor(Doctor doctor)
        {
            this.DoctorId = doctor.Id;
            this.Doctor = doctor;
        }
    }
}