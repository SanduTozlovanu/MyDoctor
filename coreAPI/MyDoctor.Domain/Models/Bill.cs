using MyDoctorApp.Domain.Helpers;

namespace MyDoctorApp.Domain.Models
{
    public class Bill
    {
        public Bill(Appointment appointment)
        {
            Id = Guid.NewGuid();
            appointment.RegisterBill(this);
        }
        public Guid Id { get; private set; }
        public virtual Appointment Appointment { get; private set; }
        public Guid AppointmentId { get; private set; }
        public double BillPrice { get; private set; }

        public void AttachAppointment(Appointment appointment)
        {
            AppointmentId = appointment.Id;
            Appointment = appointment;
        }

        public Result CalculateBillPrice(Appointment appointment)
        {
            double totalPrice = 0;
            totalPrice += appointment.Price;

            if (appointment.Prescription != null)
            {
                if (appointment.Prescription.PrescriptedDrugs != null)
                {
                    foreach (PrescriptedDrug prescriptedDrug in appointment.Prescription.PrescriptedDrugs)
                    {
                        if (prescriptedDrug.Drug == null)
                        {
                            return Result.Failure("Drug from prescriptedDrug is Null");
                        }
                        totalPrice += prescriptedDrug.Drug.Price * prescriptedDrug.Quantity;
                    }
                }
                if (appointment.Prescription.Procedures != null)
                {
                    foreach (Procedure procedure in appointment.Prescription.Procedures)
                    {
                        totalPrice += procedure.Price;
                    }
                }
            }


            BillPrice = totalPrice;
            return Result.Success();
        }
    }
}
