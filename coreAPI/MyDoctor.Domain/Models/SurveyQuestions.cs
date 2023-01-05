namespace MyDoctorApp.Domain.Models
{
    public class SurveyQuestions
    {
        private const string EMPTY_STRING = "";
        private const string CANCER_QUESTION = "Do / did you have cancer?";
        private const string BLOODPRESSURE_QUESTION = "Do you have high blood pressure?";
        private const string DIABETIS_QUESTION = "Do you have diabetis?";
        private const string ALLERGY_QUESTION = "Do you have any allergy? If yes, what are they?";
        private const string SEXUAL_QUESTION = "Do you suffer from any sexually trasmitted diseases?";
        private const string COVID_QUESTION = "Did you vaccinate yourself against COVID-19? If yes, what type of vaccine have you used?";
        private const string HEADACHE_QUESTION = "How often do you suffer from headaches?";

        public SurveyQuestions()
        {
            Id = Guid.NewGuid();
        }
        public Guid Id { get; private set; }
        public virtual Patient Patient { get; private set; }
        public Guid PatientId { get; private set; }
        public string CancerQuestion { get; private set; } = CANCER_QUESTION;
        public string CancerAnswer { get; private set; } = EMPTY_STRING;
        public string BloodPressureQuestion { get; private set; } = BLOODPRESSURE_QUESTION;
        public string BloodPressureAnswer { get; private set; } = EMPTY_STRING;
        public string DiabetisQuestion { get; private set; } = DIABETIS_QUESTION;
        public string DiabetisAnswer { get; private set; } = EMPTY_STRING;
        public string AllergiesQuestion { get; private set; } = ALLERGY_QUESTION;
        public string AllergiesAnswer { get; private set; } = EMPTY_STRING;
        public string SexualQuestion { get; private set; } = SEXUAL_QUESTION;
        public string SexualAnswer { get; private set; } = EMPTY_STRING;
        public string CovidQuestion { get; private set; } = COVID_QUESTION;
        public string CovidAnswer { get; private set; } = EMPTY_STRING;
        public string HeadAcheQuestion { get; private set; } = HEADACHE_QUESTION;
        public string HeadAcheAnswer { get; private set; } = EMPTY_STRING;
        public void AttachToPatient(Patient patient)
        {
            PatientId = patient.Id;
            Patient = patient;
        }
        public void Update(string CancerAnswer, string BloodPressureAnswer, string DiabetisAnswer,
            string AllergiesAnswer, string SexualAnswer, string CovidAnswer, string HeadAcheAnswer)
        {
            this.CancerAnswer = CancerAnswer;
            this.BloodPressureAnswer = BloodPressureAnswer;
            this.DiabetisAnswer = DiabetisAnswer;
            this.AllergiesAnswer = AllergiesAnswer;
            this.SexualAnswer = SexualAnswer;
            this.CovidAnswer = CovidAnswer;
            this.HeadAcheAnswer = HeadAcheAnswer;
        }
    }
}
