namespace MyDoctor.Domain.Models
{
    public class Bill
    {
        public Bill() 
        {
            this.Id = Guid.NewGuid();
        }
        public Guid Id { get; private set; }
        public virtual Appointment Appointment { get; private set; }
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

            if (appointment.Prescription != null)
            {
                if (appointment.Prescription.PrescriptedDrugs != null)
                {
                    foreach (PrescriptedDrug prescriptedDrug in appointment.Prescription.PrescriptedDrugs)
                    {
                        if( prescriptedDrug.Drug == null)
                        {
                            throw new ArgumentNullException("Drug");
                        }
                        totalPrice += prescriptedDrug.Drug.Price * prescriptedDrug.Quantity;
                    }
                }
                if (appointment.Prescription.Procedures!= null)       
                { 
                    foreach (Procedure procedure in appointment.Prescription.Procedures)
                    {
                        totalPrice += procedure.Price;
                    }
                }
            }


            this.BillPrice = totalPrice;
        }
    }
}
