using MyDoctor.Domain.Models;
using MyDoctor.Tests.Orderers;
using MyDoctorApp.Domain.Helpers;

namespace MyDoctor.Tests.UnitTests.DomainTests
{
    public class MedicalHistoryTest
    {
        private const string NAME = "nume";
        private const string DESCRIPTION = "desc";
        private const double PRICE = 50;
        private const uint QUANTITY = 10;

        [Fact]
        public void Create()
        {
            // When
            var mh = new MedicalHistory();

            // Then
            Assert.NotEqual(Guid.Empty, mh.Id);

            Assert.Null(mh.Patient);
            Assert.Equal(Guid.Empty, mh.PatientId);
            Assert.Empty(mh.Prescriptions);
        }

        [Fact]
        public void AttachToPatient()
        {
            // Given
            var mh = new MedicalHistory();
            var p = PatientTest.CreateDefaultPatient();

            // When
            mh.AttachToPatient(p);

            // Then
            Assert.Equal(p.Id, mh.PatientId);
            Assert.True(ReferenceEquals(p, mh.Patient));
        }

        [Fact]
        public void RegisterPrescriptions_EmptyParameter()
        {
            // Given
            var mh = new MedicalHistory();

            // When
            var actual = mh.RegisterPrescriptions(new List<Prescription>());

            // Then
            Assert.True(actual.IsFailure);
        }

        [Fact]
        public void RegisterPrescriptions_Valid()
        {
            // Given
            var mh = new MedicalHistory();

            // When
            var actual = mh.RegisterPrescriptions(new List<Prescription>()
            {
                new Prescription("",""),
                new Prescription("","")
            });

            // Then
            Assert.True(actual.IsSuccess);
        }
    }
}
