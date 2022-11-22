namespace MyDoctor.Domain.Models
{
    public class Bill
    {
        public Bill() 
        {
            this.Id = Guid.NewGuid();
        }
        public Guid Id { get; private set; }
        public Guid ApointmentId { get; private set; }
        public double BillPrice { get;}

        public void AttachAppointment(Appointment appointment) { this.ApointmentId = appointment.Id; }


    }
}
