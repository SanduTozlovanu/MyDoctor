using MyDoctorApp.Domain.Helpers;
using MyDoctorApp.Domain.Models;

namespace MyDoctor.Tests.UnitTests.DomainTests
{
    public class DoctorTest
    {
        private const string EMAIL = "ceva@gmail.com";
        private const string PASSWORD = "pass";
        private const string FIRST_NAME = "Ionut";
        private const string LAST_NAME = "Virgil";

        public static Doctor CreateDefaultDoctor()
        {
            var email = EMAIL;
            var password = PASSWORD;
            var firstName = FIRST_NAME;
            var lastName = LAST_NAME;

            return new Doctor(email, password, firstName, lastName);
        }

        [Fact]
        public void Create()
        {
            // When
            Doctor d = CreateDefaultDoctor();

            // Then
            Assert.NotNull(d);
            Assert.NotEqual(Guid.Empty, d.Id);
            Assert.Equal(EMAIL, d.Email);
            Assert.Equal(PASSWORD, d.Password);
            Assert.Equal(FIRST_NAME, d.FirstName);
            Assert.Equal(LAST_NAME, d.LastName);
            Assert.Equal(AccountTypes.Doctor, d.AccountType);

            Assert.Null(d.MedicalRoom);
            Assert.Equal(Guid.Empty, d.MedicalRoomId);
            Assert.Empty(d.Appointments);
            Assert.Empty(d.ScheduleIntervals);
        }

        [Fact]
        public void AttachToMedicalRoom()
        {
            // Given
            Doctor d = CreateDefaultDoctor();
            var mr = new MedicalRoom("");

            // When
            d.AttachToMedicalRoom(mr);

            // Then
            Assert.Equal(mr.Id, d.MedicalRoomId);
            Assert.True(ReferenceEquals(mr, d.MedicalRoom));
        }

        [Fact]
        public void RegisterAppointment()
        {
            // Given
            Doctor d = CreateDefaultDoctor();
            var ap = new Appointment();

            // When
            d.RegisterAppointment(ap);

            // Then
            Assert.True(d.Appointments.Count == 1);
            Assert.Contains(ap, d.Appointments);
            Assert.True(ReferenceEquals(d, ap.Doctor));
        }

        [Fact]
        public void Update()
        {
            // Given
            Doctor d = CreateDefaultDoctor();
            string email = "email";
            string password = "password";
            string firstName = "firstName";
            string lastName = "lastName";

            Doctor newDoctor = new(email, password, firstName, lastName);

            // When
            d.Update(newDoctor);

            // Then
            Assert.Equal(newDoctor.Email, d.Email);
            Assert.Equal(newDoctor.Password, d.Password);
            Assert.Equal(newDoctor.FirstName, d.FirstName);
            Assert.Equal(newDoctor.LastName, d.LastName);
            Assert.Equal(newDoctor.Speciality, d.Speciality);
        }
    }
}
