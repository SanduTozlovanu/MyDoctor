using MyDoctorApp.Domain.Helpers;

namespace MyDoctorApp.Domain.Models
{
    public class Appointment
    {
        public Appointment(double price) //NOSONAR
        {
            Id = Guid.NewGuid();
            Price = price;
        }
        public Guid Id { get; private set; }

        public virtual Patient Patient { get; private set; }
        public Guid PatientId { get; private set; }
        public virtual Doctor Doctor { get; private set; }
        public Guid DoctorId { get; private set; }
        public virtual AppointmentInterval AppointmentInterval { get; private set; }
        public double Price { get; private set; }
        public virtual Prescription? Prescription { get; private set; }
        public virtual Bill? Bill { get; private set; }

        public void AttachToPatient(Patient patient)
        {
            PatientId = patient.Id;
            Patient = patient;
        }
        public void AttachToDoctor(Doctor doctor)
        {
            DoctorId = doctor.Id;
            Doctor = doctor;
        }
        public void RegisterPrescription(Prescription prescription)
        {
            prescription.AttachAppointment(this);
            Prescription = prescription;
            CalculateBillPrice();
        }
        public void RegisterBill(Bill bill)
        {
            bill.AttachAppointment(this);
            Bill = bill;
            CalculateBillPrice();
        }
        public void RegisterAppointmentInterval(AppointmentInterval appointmentInterval)
        {
            appointmentInterval.AttachToAppointment(this);
            AppointmentInterval = appointmentInterval;
        }

        public Result CalculateBillPrice()
        {
            if (Bill == null)
            {
                return Result.Failure("Not enough data to make the billing.");
            }

            Bill.CalculateBillPrice(this);

            return Result.Success();
        }

    }
}
