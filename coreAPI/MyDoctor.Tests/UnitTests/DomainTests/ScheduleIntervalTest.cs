using MyDoctorApp.Domain.Helpers;
using MyDoctorApp.Domain.Models;

namespace MyDoctor.Tests.UnitTests.DomainTests
{
    public class ScheduleIntervalTest
    {
        private DateTime Now = DateTime.Now;
        private string WeekDay;
        private TimeOnly StartTime;
        private TimeOnly EndTime;

        public ScheduleIntervalTest()
        {
            WeekDay = WeekDays.Monday.ToString();
            StartTime = TimeOnly.FromDateTime(Now);
            EndTime = TimeOnly.FromDateTime(Now);
        }


        [Fact]
        public void Create()
        {
            // When
            var si = new ScheduleInterval(WeekDay, StartTime, EndTime);

            // Then
            Assert.NotEqual(Guid.Empty, si.Id);
            Assert.Equal(WeekDay, si.DayOfWeek);
            Assert.Equal(StartTime, si.StartTime);
            Assert.Equal(EndTime, si.EndTime);

            Assert.Null(si.Doctor);
            Assert.Equal(Guid.Empty, si.DoctorId);
        }

        [Fact]
        public void AttachToDoctor()
        {
            // Given
            var si = new ScheduleInterval(WeekDay, StartTime, EndTime);
            var d = DoctorTest.CreateDefaultDoctor();

            // When
            si.AttachToDoctor(d);

            // Then
            Assert.Equal(d.Id, si.DoctorId);
            Assert.True(ReferenceEquals(d, si.Doctor));
        }
    }
}
