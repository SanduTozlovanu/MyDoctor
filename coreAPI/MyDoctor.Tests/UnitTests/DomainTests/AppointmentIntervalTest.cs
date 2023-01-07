using MyDoctorApp.Domain.Models;

namespace MyDoctor.Tests.UnitTests.DomainTests
{
    public class AppointmentIntervalTest
    {
        private static readonly DateOnly date = DateOnly.Parse("2023-04-20");
        private static readonly TimeOnly startTime = TimeOnly.Parse("06:00");
        private static readonly TimeOnly endTime = TimeOnly.Parse("07:00");
        public static AppointmentInterval CreateDefaultAppointmentInterval()
        {
            return new AppointmentInterval(date,
                startTime, endTime);
        }
        [Fact]
        public void Create()
        {
            // When
            AppointmentInterval ap = CreateDefaultAppointmentInterval();

            // Then
            Assert.NotNull(ap);
            Assert.NotEqual(Guid.Empty, ap.Id);

            Assert.Null(ap.Appointment);
            Assert.Equal(Guid.Empty, ap.AppointmentId);
            Assert.Equal(date, ap.Date);
            Assert.Equal(startTime, ap.StartTime);
            Assert.Equal(endTime, ap.EndTime);
        }

        [Fact]
        public void AttachToAppointment()
        {
            // Given
            AppointmentInterval ap = CreateDefaultAppointmentInterval();
            Appointment appointment = AppointmentTest.CreateDefaultAppointment();
            ap.AttachToAppointment(appointment);

            Assert.True(ReferenceEquals(ap.Appointment, appointment));
            Assert.Equal(ap.AppointmentId, appointment.Id);
        }
    }
}
