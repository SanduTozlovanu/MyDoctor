namespace MyDoctor.Application.Response
{
    public class SurveyQuestionsResponse
    {
        public Guid Id { get; set; }
        public Guid PatientId { get; set; }
        public string CancerQuestion { get; private set; }
        public string CancerAnswer { get; private set; }
        public string BloodPressureQuestion { get; private set; }
        public string BloodPressureAnswer { get; private set; }
        public string DiabetisQuestion { get; private set; }
        public string DiabetisAnswer { get; private set; }
        public string AllergiesQuestion { get; private set; }
        public string AllergiesAnswer { get; private set; }
        public string SexualQuestion { get; private set; }
        public string SexualAnswer { get; private set; }
        public string CovidQuestion { get; private set; }
        public string CovidAnswer { get; private set; }
        public string HeadAcheQuestion { get; private set; }
        public string HeadAcheAnswer { get; private set; }

        public override bool Equals(object? obj)
        {
            return obj is SurveyQuestionsResponse dto &&
                   Id.Equals(dto.Id) &&
                   PatientId.Equals(dto.PatientId) &&
                   CancerQuestion == dto.CancerQuestion &&
                   CancerAnswer == dto.CancerAnswer &&
                   BloodPressureQuestion == dto.BloodPressureQuestion &&
                   BloodPressureAnswer == dto.BloodPressureAnswer &&
                   DiabetisQuestion == dto.DiabetisQuestion &&
                   DiabetisAnswer == dto.DiabetisAnswer &&
                   AllergiesQuestion == dto.AllergiesQuestion &&
                   AllergiesAnswer == dto.AllergiesAnswer &&
                   SexualQuestion == dto.SexualQuestion &&
                   SexualAnswer == dto.SexualAnswer &&
                   CovidQuestion == dto.CovidQuestion &&
                   CovidAnswer == dto.CovidAnswer &&
                   HeadAcheQuestion == dto.HeadAcheQuestion &&
                   HeadAcheAnswer == dto.HeadAcheAnswer;
        }

        public override int GetHashCode()
        {
            HashCode hash = new();
            hash.Add(Id);
            hash.Add(PatientId);
            hash.Add(CancerQuestion);
            hash.Add(CancerAnswer);
            hash.Add(BloodPressureQuestion);
            hash.Add(BloodPressureAnswer);
            hash.Add(DiabetisQuestion);
            hash.Add(DiabetisAnswer);
            hash.Add(AllergiesQuestion);
            hash.Add(AllergiesAnswer);
            hash.Add(SexualQuestion);
            hash.Add(SexualAnswer);
            hash.Add(CovidQuestion);
            hash.Add(CovidAnswer);
            hash.Add(HeadAcheQuestion);
            hash.Add(HeadAcheAnswer);
            return hash.ToHashCode();
        }
    }
}
