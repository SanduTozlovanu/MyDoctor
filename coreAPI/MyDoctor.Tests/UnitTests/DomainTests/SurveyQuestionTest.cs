using MyDoctorApp.Domain.Models;

namespace MyDoctor.Tests.UnitTests.DomainTests
{
    public class SurveyQuestionTest
    {

        public static SurveyQuestion CreateDefaultSurveyQuestion()
        {
            return new SurveyQuestion(SurveyQuestion.GetQuestionBody(SurveyQuestion.Question.CancerQuestion));
        }

        [Fact]
        public void Create()
        {
            // When
            var mh = CreateDefaultSurveyQuestion();

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
            var mh = CreateDefaultSurveyQuestion();
            var p = PatientTest.CreateDefaultPatient();

            // When
            mh.AttachToPatient(p);

            // Then
            Assert.Equal(p.Id, mh.PatientId);
            Assert.True(ReferenceEquals(p, mh.Patient));
        }
        [Fact]
        public void Update()
        {
            // Given
            var mh = CreateDefaultSurveyQuestion();
            Assert.Equal("", mh.Answer);
            string answer = "No, i dont cancer";
            mh.Update(answer);
            Assert.Equal(answer, mh.Answer);
        }
        [Fact]
        public void GetQuestionBodyTest()
        {
            Assert.Equal(SurveyQuestion.CANCER_QUESTION, SurveyQuestion.GetQuestionBody(SurveyQuestion.Question.CancerQuestion));
            Assert.Equal(SurveyQuestion.ALLERGY_QUESTION, SurveyQuestion.GetQuestionBody(SurveyQuestion.Question.AllergyQuestion));
            Assert.Equal(SurveyQuestion.SEXUAL_QUESTION, SurveyQuestion.GetQuestionBody(SurveyQuestion.Question.SexualQuestion));
            Assert.Equal(SurveyQuestion.COVID_QUESTION, SurveyQuestion.GetQuestionBody(SurveyQuestion.Question.CovidQuestion));
            Assert.Equal(SurveyQuestion.BLOODPRESSURE_QUESTION, SurveyQuestion.GetQuestionBody(SurveyQuestion.Question.BloodPressureQuestion));
            Assert.Equal(SurveyQuestion.DIABETIS_QUESTION, SurveyQuestion.GetQuestionBody(SurveyQuestion.Question.DiabetisQuestion));
            Assert.Equal(SurveyQuestion.HEADACHE_QUESTION, SurveyQuestion.GetQuestionBody(SurveyQuestion.Question.HeadAcheQuestion));
        }
    }
}
