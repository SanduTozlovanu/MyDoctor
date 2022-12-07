using MyDoctor.Domain.Models;
using MyDoctor.Tests.Orderers;
using MyDoctorApp.Domain.Helpers;

namespace MyDoctor.Tests.UnitTests.DomainTests
{
    public class PrescriptionTest
    {
        private const string NAME = "name";
        private const string DESCRIPTION = "desc";

        [Fact]
        public void Create()
        {
            // When
            var pre = new Prescription(NAME, DESCRIPTION);

            // Then
            Assert.NotEqual(Guid.Empty, pre.Id);
            Assert.Equal(NAME, pre.Name);
            Assert.Equal(DESCRIPTION, pre.Description);

            Assert.Empty(pre.PrescriptedDrugs);
            Assert.Empty(pre.Procedures);
            Assert.Null(pre.Appointment);
            Assert.Equal(Guid.Empty, pre.AppointmentId);
        }

        [Fact]
        public void AttachAppointment()
        {
            // Given
            var pre = new Prescription(NAME, DESCRIPTION);
            var ap = new Appointment(10);

            // When
            pre.AttachAppointment(ap);

            // Then
            Assert.Equal(ap.Id, pre.AppointmentId);
            Assert.True(ReferenceEquals(ap, pre.Appointment));
        }

        [Fact]
        public void RegisterProcedures_EmptyParameter()
        {
            // Given
            var pre = new Prescription(NAME, DESCRIPTION);

            // When
            var actual = pre.RegisterProcedures(new List<Procedure>());

            // Then
            Assert.True(actual.IsFailure);
        }

        [Fact]
        public void RegisterProcedures_Valid()
        {
            // Given
            var pre = new Prescription(NAME, DESCRIPTION);

            // When
            var actual = pre.RegisterProcedures(new List<Procedure>()
            {
                new Procedure("", "", 5)
            });

            // Then
            Assert.True(actual.IsSuccess);
        }

        [Fact]
        public void RegisterPrescriptedDrugs_EmptyParameter()
        {
            // Given
            var pre = new Prescription(NAME, DESCRIPTION);

            // When
            var actual = pre.RegisterPrescriptedDrugs(new List<PrescriptedDrug>());

            // Then
            Assert.True(actual.IsFailure);
        }

        [Fact]
        public void RegisterPrescriptedDrugs_Valid()
        {
            // Given
            var pre = new Prescription(NAME, DESCRIPTION);

            // When
            var actual = pre.RegisterPrescriptedDrugs(new List<PrescriptedDrug>()
            {
                new PrescriptedDrug(10)
            });

            // Then
            Assert.True(actual.IsSuccess);
        }
    }
}
