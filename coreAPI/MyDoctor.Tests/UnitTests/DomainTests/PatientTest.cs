using MyDoctor.Domain.Models;

namespace MyDoctor.Tests.UnitTests.DomainTests
{
    public class PatientTest
    {
        private Patient CreateDefaultPatient()
        {
            var email = "ceva@gmail.com";
            var password = "pass";
            var firstName = "Ionut";
            var lastName = "Virgil";
            uint age = 10;

            return new Patient(email, password, firstName, lastName, age);
        }

        [Fact]
        public void Create()
        {
            // When
            var p = CreateDefaultPatient();

            // Then
            Assert.NotNull(p);
        }

        [Fact]
        public void RegisterMedicalHistory()
        {
            // Given
            var p = CreateDefaultPatient();
            var mh = new MedicalHistory();

            // When
            p.RegisterMedicalHistory(mh);

            // Then
            Assert.True(Object.ReferenceEquals(mh, p.MedicalHistory));
        }

        [Fact]
        public void RegisterAppointment()
        {
            // Given
            var p = CreateDefaultPatient();
            var ap = new Appointment(10);

            // When
            p.RegisterAppointment(ap);

            // Then
            Assert.True(p.Appointments.Count == 1);
            Assert.Contains(ap, p.Appointments);
        }
    }
}
