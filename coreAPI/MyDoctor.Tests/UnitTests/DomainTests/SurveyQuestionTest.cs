using MyDoctorApp.Domain.Models;

namespace MyDoctor.Tests.UnitTests.DomainTests
{
    public class SurveyQuestionTest
    {

        [Fact]
        public void Create()
        {
            // When
            var mh = new SurveyQuestions();

            // Then
            Assert.NotEqual(Guid.Empty, mh.Id);

            Assert.Null(mh.Patient);
            Assert.Equal(Guid.Empty, mh.PatientId);
        }

        [Fact]
        public void AttachToPatient()
        {
            // Given
            var mh = new SurveyQuestions();
            var p = PatientTest.CreateDefaultPatient();

            // When
            mh.AttachToPatient(p);

            // Then
            Assert.Equal(p.Id, mh.PatientId);
            Assert.True(ReferenceEquals(p, mh.Patient));
        }
    }
}
