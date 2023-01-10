using MyDoctorApp.Domain.Helpers;

namespace MyDoctorApp.Domain.Models
{
    public class Bill
    {
        private const string NULL_DOCTORFIELD_ERROR = "Doctor field for Bill instance is null!";
        private const string NULL_PRESCRIPTEDDRUG_ERROR = "Drug from prescriptedDrug is Null";

        public Bill()
        {
            Id = Guid.NewGuid();
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
            if(appointment.Doctor is null)
            {
                throw new NullReferenceException(NULL_DOCTORFIELD_ERROR);
            }
            totalPrice += appointment.Doctor.AppointmentPrice;

            var result = CalculatePrescriptedDrugsAndProceduresPrice(appointment, ref totalPrice);
            if (result.IsFailure)
            {
                return result;
            }

            BillPrice = totalPrice;
            return Result.Success();
        }
        
        private static Result CalculatePrescriptedDrugsAndProceduresPrice(Appointment appointment, ref double totalPrice)
        {
            if (appointment.Prescription == null)
                return Result.Success();
            if (appointment.Prescription.PrescriptedDrugs != null)
            {
                foreach (PrescriptedDrug prescriptedDrug in appointment.Prescription.PrescriptedDrugs)
                {
                    if (prescriptedDrug.Drug == null)
                    {
                        return Result.Failure(NULL_PRESCRIPTEDDRUG_ERROR);
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
            return Result.Success();
        }
    }
}
