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
        public Guid PatientId { get; private set; }
        public Guid DoctorId { get; private set; }
        public Guid AppointmentIntervalId { get; private set; }
        public double Price { get; private set; }
        public Guid? PrescriptionId { get; private set; }
        public Guid BillId { get; private set; }

        public void AttachPatient(Patient patient) { this.PatientId = patient.Id; }
        public void AttachDoctor(Doctor doctor) { this.DoctorId = doctor.Id; }
        public void RegisterPrescription(Prescription prescription) 
        {
            this.PrescriptionId = prescription.Id;
            prescription.AttachAppointment(this);
        }
        public void RegisterBill(Bill bill) 
        {
            this.BillId = bill.Id; 
            bill.AttachAppointment(this);
        }
        public void RegisterAppointmentInterval (AppointmentInterval appointmentInterval) 
        { 
            this.AppointmentIntervalId = appointmentInterval.Id; 
            appointmentInterval.AttachAppointment(this);
        }


    }
}
