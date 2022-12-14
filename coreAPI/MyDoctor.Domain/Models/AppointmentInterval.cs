using static System.Runtime.InteropServices.JavaScript.JSType;

namespace MyDoctorApp.Domain.Models
{
    public class AppointmentInterval : Interval
    {
        public AppointmentInterval(DateOnly date, TimeOnly startTime, TimeOnly endTime) : base(startTime, endTime)
        {
            Date = date;
        }

        public DateOnly Date { get; private set; }
        public virtual Appointment Appointment { get; private set; }
        public Guid AppointmentId { get; private set; }

        public void AttachToAppointment(Appointment appointment)
        {
            AppointmentId = appointment.Id;
            Appointment = appointment;
        }
    }
}