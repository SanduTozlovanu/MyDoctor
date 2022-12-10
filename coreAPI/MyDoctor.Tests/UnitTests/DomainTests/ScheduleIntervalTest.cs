using MyDoctorApp.Domain.Models;

namespace MyDoctor.Tests.UnitTests.DomainTests
{
    public class ScheduleIntervalTest
    {
        private DateTime Now = DateTime.Now;
        private DateOnly Date;
        private TimeOnly StartTime;
        private TimeOnly EndTime;

        public ScheduleIntervalTest()
        {
            Date = DateOnly.FromDateTime(Now);
            StartTime = TimeOnly.FromDateTime(Now);
            EndTime = TimeOnly.FromDateTime(Now);
        }


        [Fact]
        public void Create()
        {
            // When
            var si = new ScheduleInterval(Date, StartTime, EndTime, 10);

            // Then
            Assert.NotEqual(Guid.Empty, si.Id);
            Assert.Equal(Date, si.Date);
            Assert.Equal(StartTime, si.StartTime);
            Assert.Equal(EndTime, si.EndTime);
            Assert.Equal(10, (int)si.AppointmentDuration);

            Assert.Null(si.Doctor);
            Assert.Equal(Guid.Empty, si.DoctorId);
        }

        [Fact]
        public void AttachToDoctor()
        {
            // Given
            var si = new ScheduleInterval(Date, StartTime, EndTime, 15);
            var d = DoctorTest.CreateDefaultDoctor();

            // When
            si.AttachToDoctor(d);

            // Then
            Assert.Equal(d.Id, si.DoctorId);
            Assert.True(ReferenceEquals(d, si.Doctor));
        }
    }
}
