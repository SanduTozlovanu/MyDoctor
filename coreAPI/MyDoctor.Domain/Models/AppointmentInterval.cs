namespace MyDoctorApp.Domain.Models
{
    public class AppointmentInterval : Interval
    {
        public AppointmentInterval(DateOnly date, TimeOnly startTime, TimeOnly endTime) : base(date, startTime, endTime) { }
        public virtual Appointment Appointment { get; private set; }
        public Guid AppointmentId { get; private set; }

        public void AttachToAppointment(Appointment appointment)
        {
            AppointmentId = appointment.Id;
            Appointment = appointment;
        }
    }
}