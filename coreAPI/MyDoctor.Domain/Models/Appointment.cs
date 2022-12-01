using MyDoctorApp.Domain.Helpers;

namespace MyDoctor.Domain.Models
{
    public class Appointment
    {
        public Appointment(double price) 
        {
            this.Id = Guid.NewGuid();
            this.Price = price;
        }
        public Guid Id { get; private set; } 

        public virtual Patient Patient { get; private set; }
        public Guid PatientId { get; private set; }
        public virtual Doctor Doctor { get; private set; }
        public Guid DoctorId { get; private set; }
        public AppointmentInterval AppointmentInterval { get; private set; }
        public double Price { get; private set; }
        public Prescription Prescription { get; private set; }
        public Bill Bill { get; private set; }

        public void AttachToPatient(Patient patient) {
            this.PatientId = patient.Id;
            this.Patient = patient;
        }
        public void AttachToDoctor(Doctor doctor) {
            this.DoctorId = doctor.Id;
            this.Doctor = doctor;
        }
        public void RegisterPrescription(Prescription prescription) 
        {
            prescription.AttachAppointment(this);
            this.Prescription = prescription;
            CalculateBillPrice();
        }
        public void RegisterBill(Bill bill) 
        {
            bill.AttachAppointment(this);
            this.Bill = bill;
            CalculateBillPrice();
        }
        public void RegisterAppointmentInterval (AppointmentInterval appointmentInterval) 
        { 
            appointmentInterval.AttachToAppointment(this);
            this.AppointmentInterval = appointmentInterval;
        }

        public Result CalculateBillPrice()
        {
            if (this.Bill == null)
            {
                return Result.Failure("Not enough data to make the billing.");
            }

            this.Bill.CalculateBillPrice(this);

            return Result.Success();
        }

    }
}
