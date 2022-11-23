namespace MyDoctor.Domain.Models
{
    public class Bill
    {
        public Bill() 
        {
            this.Id = Guid.NewGuid();
        }
        public Guid Id { get; private set; }
        public Appointment Appointment { get; private set; }
        public Guid AppointmentId { get; private set; }
        public double BillPrice { get; private set; }

        public void AttachAppointment(Appointment appointment) {
            this.AppointmentId = appointment.Id;
            this.Appointment = appointment;
        }

        public void CalculateBillPrice(Appointment appointment)
        {
            double totalPrice = 0;
            totalPrice += appointment.Price;

            foreach (Drug drug in appointment.Prescription.Drugs)
            {
                totalPrice += (drug.Price * drug.Quantity);
            }

            this.BillPrice = totalPrice;
        }
    }
}
