using MyDoctor.Domain.Models;

namespace MyDoctor
{
    public class AppointmentInterval
    {
        public AppointmentInterval(DateOnly date, TimeOnly startTime, TimeOnly endTime) 
        {
            this.Id = Guid.NewGuid();
            this.Date = date;
            this.StartTime = startTime;
            this.EndTime = endTime;
        }
        public Guid Id { get; private set; }
        public Guid? AppointmentId { get; private set; }
        public Guid DoctorId { get; private set; }
        public DateOnly Date { get; private set; }
        public TimeOnly StartTime { get; private set; }
        public TimeOnly EndTime { get; private set; }

        public void AttachAppointment(Appointment appointment)
        {
            this.AppointmentId = appointment.Id;
        }
        public void AttachDoctor(Doctor doctor)
        {
            this.DoctorId = doctor.Id;
        }
    }
}