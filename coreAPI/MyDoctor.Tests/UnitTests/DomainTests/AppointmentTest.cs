using MyDoctorApp.Domain.Models;

namespace MyDoctor.Tests.UnitTests.DomainTests
{
    public class AppointmentTest
    {

        public static Appointment CreateDefaultAppointment()
        {
            var appointment = new Appointment();
            var doctor = DoctorTest.CreateDefaultDoctor();
            doctor.RegisterAppointment(appointment);
            return appointment;
        }

        [Fact]
        public void Create()
        {
            // When
            Appointment ap = CreateDefaultAppointment();

            // Then
            Assert.NotNull(ap);
            Assert.NotEqual(Guid.Empty, ap.Id);

            Assert.Null(ap.Patient);
            Assert.Equal(Guid.Empty, ap.PatientId);
            Assert.NotNull(ap.Doctor);
            Assert.Null(ap.AppointmentInterval);
            Assert.Null(ap.Prescription);
            Assert.Null(ap.Bill);
        }

        [Fact]
        public void AttachToPatient()
        {
            // Given
            Appointment ap = CreateDefaultAppointment();
            Patient p = PatientTest.CreateDefaultPatient();

            // When
            ap.AttachToPatient(p);

            // Then
            Assert.Equal(p.Id, ap.PatientId);
            Assert.True(ReferenceEquals(p, ap.Patient));
        }

        [Fact]
        public void AttachToDoctor()
        {
            // Given
            Appointment ap = CreateDefaultAppointment();
            Doctor d = DoctorTest.CreateDefaultDoctor();

            // When
            ap.AttachToDoctor(d);

            // Then
            Assert.Equal(d.Id, ap.DoctorId);
            Assert.True(ReferenceEquals(d, ap.Doctor));
        }

        [Fact]
        public void RegisterBill()
        {
            // Given
            Appointment ap = CreateDefaultAppointment();
            Bill b = new();

            // When
            ap.RegisterBill(b);

            // Then
            Assert.Equal(ap.Id, b.AppointmentId);
            Assert.True(ReferenceEquals(ap, b.Appointment));
            Assert.True(ReferenceEquals(b, ap.Bill));
        }

        [Fact]
        public void RegisterAppointmentInterval()
        {
            // Given
            DateTime now = DateTime.Now;
            Appointment ap = CreateDefaultAppointment();
            AppointmentInterval ai = new(
                DateOnly.FromDateTime(now),
                TimeOnly.FromDateTime(now),
                TimeOnly.FromDateTime(now));

            // When
            ap.RegisterAppointmentInterval(ai);

            // Then
            Assert.Equal(ap.Id, ai.AppointmentId);
            Assert.True(ReferenceEquals(ap, ai.Appointment));
            Assert.True(ReferenceEquals(ai, ap.AppointmentInterval));
        }

        [Fact]
        public void RegisterPrescription()
        {
            // Given
            Appointment ap = CreateDefaultAppointment();
            Prescription p = new("", "");

            // When
            ap.RegisterPrescription(p);

            // Then
            Assert.Equal(ap.Id, p.AppointmentId);
            Assert.True(ReferenceEquals(ap, p.Appointment));
            Assert.True(ReferenceEquals(p, ap.Prescription));
        }

        [Fact]
        public void CalculateBillPrice_WithoutBill()
        {
            // Given
            Appointment ap = CreateDefaultAppointment();

            // When
            var actual = ap.CalculateBillPrice();

            // Then
            Assert.True(actual.IsFailure);
        }

        [Fact]
        public void CalculateBillPrice_WithBill()
        {
            // Given
            Appointment ap = CreateDefaultAppointment();
            Bill b = new();
            ap.RegisterBill(b);

            // When
            var actual = ap.CalculateBillPrice();

            // Then
            Assert.True(actual.IsSuccess);
        }

    }
}
