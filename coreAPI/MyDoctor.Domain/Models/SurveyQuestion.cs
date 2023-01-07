namespace MyDoctorApp.Domain.Models
{
    public class SurveyQuestion
    {
        public const string EMPTY_STRING = "";
        public const string CANCER_QUESTION = "Do / did you have cancer?";
        public const string BLOODPRESSURE_QUESTION = "Do you have high blood pressure?";
        public const string DIABETIS_QUESTION = "Do you have diabetis?";
        public const string ALLERGY_QUESTION = "Do you have any allergy? If yes, what are they?";
        public const string SEXUAL_QUESTION = "Do you suffer from any sexually trasmitted diseases?";
        public const string COVID_QUESTION = "Did you vaccinate yourself against COVID-19? If yes, what type of vaccine have you used?";
        public const string HEADACHE_QUESTION = "How often do you suffer from headaches?";

        public enum Question
        {
            CancerQuestion,
            BloodPressureQuestion,
            DiabetisQuestion,
            AllergyQuestion,
            SexualQuestion,
            CovidQuestion,
            HeadAcheQuestion
        }
        public SurveyQuestion(string questionBody)
        {
            Id = Guid.NewGuid();
            QuestionBody = questionBody;
        }
        public Guid Id { get; private set; }
        public virtual Patient Patient { get; private set; }
        public Guid PatientId { get; private set; }
        public string QuestionBody { get; private set; }
        public string Answer { get; private set; } = EMPTY_STRING;
        public void AttachToPatient(Patient patient)
        {
            PatientId = patient.Id;
            Patient = patient;
        }
        public void Update(string answer)
        {
            this.Answer = answer;
        }

        public static string GetQuestionBody(Question question)
        {
            return question switch
            {
                Question.CancerQuestion => CANCER_QUESTION,
                Question.BloodPressureQuestion => BLOODPRESSURE_QUESTION,
                Question.DiabetisQuestion => DIABETIS_QUESTION,
                Question.CovidQuestion => COVID_QUESTION,
                Question.SexualQuestion => SEXUAL_QUESTION,
                Question.AllergyQuestion => ALLERGY_QUESTION,
                Question.HeadAcheQuestion => HEADACHE_QUESTION,
                _ => EMPTY_STRING,
            };
        }
    }
}
