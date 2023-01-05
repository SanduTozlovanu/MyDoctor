using MyDoctorApp.Domain.Helpers;
using MyDoctorApp.Domain.Models;

namespace MyDoctor.Tests.UnitTests.DomainTests
{
    public class PatientTest
    {
        private const string EMAIL = "ceva@gmail.com";
        private const string PASSWORD = "pass";
        private const string FIRST_NAME = "Ionut";
        private const string LAST_NAME = "Virgil";

        public static Patient CreateDefaultPatient()
        {
            var email = EMAIL;
            var password = PASSWORD;
            var firstName = FIRST_NAME;
            var lastName = LAST_NAME;

            return new Patient(email, password, firstName, lastName);
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
            Assert.Equal(AccountTypes.Patient, p.AccountType);

            Assert.Empty(p.SurveyQuestions);
            Assert.Empty(p.Appointments);
        }

        [Fact]
        public void RegisterSurveyQuestions()
        {
            // Given
            Patient p = CreateDefaultPatient();
            var surveyQuestionList = new List<SurveyQuestion> { new SurveyQuestion(SurveyQuestion.GetQuestionBody(SurveyQuestion.Question.CancerQuestion)) };

            // When
            p.RegisterSurveyQuestions(surveyQuestionList);

            // Then
            Assert.Equal(surveyQuestionList, p.SurveyQuestions);
        }

        [Fact]
        public void RegisterAppointment()
        {
            // Given
            Patient p = CreateDefaultPatient();
            var ap = new Appointment();

            // When
            p.RegisterAppointment(ap);

            // Then
            Assert.True(p.Appointments.Count == 1);
            Assert.Contains(ap, p.Appointments);
            Assert.True(ReferenceEquals(p, ap.Patient));
        }

        [Fact]
        public void Update()
        {
            // Given
            Patient p = CreateDefaultPatient();
            string email = "email";
            string password = "password";
            string firstName = "firstName";
            string lastName = "lastName";

            Patient newPatient = new(email, password, firstName, lastName);

            // When
            p.Update(newPatient);

            // Then
            Assert.Equal(newPatient.Email, p.Email);
            Assert.Equal(newPatient.Password, p.Password);
            Assert.Equal(newPatient.FirstName, p.FirstName);
            Assert.Equal(newPatient.LastName, p.LastName);
        }
    }
}
