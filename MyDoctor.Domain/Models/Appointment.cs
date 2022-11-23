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
        public virtual AppointmentInterval AppointmentInterval { get; private set; }
        public Guid AppointmentIntervalId { get; private set; }
        public double Price { get; private set; }
        public virtual Prescription Prescription { get; private set; }
        public Guid? PrescriptionId { get; private set; }
        public virtual Bill Bill { get; private set; }
        public Guid BillId { get; private set; }

        public void AttachPatient(Patient patient) {
            this.PatientId = patient.Id;
            this.Patient= patient;
        }
        public void AttachDoctor(Doctor doctor) {
            this.DoctorId = doctor.Id;
            this.Doctor = doctor;
        }
        public void RegisterPrescription(Prescription prescription) 
        {
            this.PrescriptionId = prescription.Id;
            prescription.AttachAppointment(this);
            this.Prescription = prescription;
        }
        public void RegisterBill(Bill bill) 
        {
            this.BillId = bill.Id; 
            bill.AttachAppointment(this);
            this.Bill = bill;
        }
        public void RegisterAppointmentInterval (AppointmentInterval appointmentInterval) 
        { 
            this.AppointmentIntervalId = appointmentInterval.Id; 
            appointmentInterval.AttachAppointment(this);
            this.AppointmentInterval = appointmentInterval;
        }

        public void CalculateBillPrice()
        {
            this.Bill.CalculateBillPrice(this);
        }

    }
}
