using MyDoctor.Domain.Models;
using MyDoctor.Tests.Orderers;
using MyDoctorApp.Domain.Helpers;

namespace MyDoctor.Tests.UnitTests.DomainTests
{
    public class PatientTest
    {
        private const string EMAIL = "ceva@gmail.com";
        private const string PASSWORD = "pass";
        private const string FIRST_NAME = "Ionut";
        private const string LAST_NAME = "Virgil";
        private const uint AGE = 10;

        public static Patient CreateDefaultPatient()
        {
            var email = EMAIL;
            var password = PASSWORD;
            var firstName = FIRST_NAME;
            var lastName = LAST_NAME;
            uint age = AGE;

            return new Patient(email, password, firstName, lastName, age);
        }

        [Fact]
        public void Create()
        {
            // When
            Patient p = CreateDefaultPatient();

            // Then
            Assert.NotNull(p);
            Assert.NotEqual(Guid.Empty, p.Id);
            Assert.Equal(EMAIL, p.Email);
            Assert.Equal(PASSWORD, p.Password);
            Assert.Equal(FIRST_NAME, p.FirstName);
            Assert.Equal(LAST_NAME, p.LastName);
            Assert.Equal(AGE, p.Age);
            Assert.Equal(AccountTypes.Patient, p.AccountType);

            Assert.Null(p.MedicalHistory);
            Assert.Empty(p.Appointments);
        }

        [Fact]
        public void RegisterMedicalHistory()
        {
            // Given
            Patient p = CreateDefaultPatient();
            var mh = new MedicalHistory();

            // When
            p.RegisterMedicalHistory(mh);

            // Then
            Assert.True(ReferenceEquals(mh, p.MedicalHistory));
        }

        [Fact]
        public void RegisterAppointment()
        {
            // Given
            Patient p = CreateDefaultPatient();
            var ap = new Appointment(10);

            // When
            p.RegisterAppointment(ap);

            // Then
            Assert.True(p.Appointments.Count == 1);
            Assert.Contains(ap, p.Appointments);
            Assert.True(ReferenceEquals(p, ap.Patient));
        }
    }
}
