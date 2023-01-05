using MyDoctorApp.Domain.Models;

namespace MyDoctor.Tests.UnitTests.DomainTests
{
    public class SurveyQuestionTest
    {

        [Fact]
        public void Create()
        {
            // When
            var mh = new SurveyQuestion(SurveyQuestion.GetQuestionBody(SurveyQuestion.Question.CancerQuestion));

            // Then
            Assert.NotEqual(Guid.Empty, mh.Id);

            Assert.Null(mh.Patient);
            Assert.Equal(Guid.Empty, mh.PatientId);
            Assert.Equal(SurveyQuestion.GetQuestionBody(SurveyQuestion.Question.CancerQuestion), mh.QuestionBody);
        }

        [Fact]
        public void AttachToPatient()
        {
            // Given
            var mh = new SurveyQuestion(SurveyQuestion.GetQuestionBody(SurveyQuestion.Question.CancerQuestion));
            var p = PatientTest.CreateDefaultPatient();

            // When
            mh.AttachToPatient(p);

            // Then
            Assert.Equal(p.Id, mh.PatientId);
            Assert.True(ReferenceEquals(p, mh.Patient));
        }
    }
}
