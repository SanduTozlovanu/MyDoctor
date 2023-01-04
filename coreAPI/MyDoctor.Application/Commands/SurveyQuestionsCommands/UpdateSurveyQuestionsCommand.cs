using MediatR;
using MyDoctor.Application.Response;

namespace MyDoctor.Application.Commands.SurveyQuestionsCommands
{
    public class UpdateSurveyQuestionsCommand : IRequest<SurveyQuestionsResponse>
    {
        public UpdateSurveyQuestionsCommand(Guid patientId, string cancerAnswer, string bloodPressureAnswer,
            string diabetisAnswer, string allergiesAnswer, string sexualAnswer, string covidAnswer, string headAcheAnswer) 
        { 
            PatientId = patientId;
            CancerAnswer = cancerAnswer;
            BloodPressureAnswer = bloodPressureAnswer;
            DiabetisAnswer= diabetisAnswer;
            AllergiesAnswer= allergiesAnswer;
            SexualAnswer= sexualAnswer;
            CovidAnswer= covidAnswer;
            HeadAcheAnswer= headAcheAnswer;
        }
        public Guid PatientId { get; private set; }
        public string CancerAnswer { get; private set; }
        public string BloodPressureAnswer { get; private set; }
        public string DiabetisAnswer { get; private set; }
        public string AllergiesAnswer { get; private set; }
        public string SexualAnswer { get; private set; }
        public string CovidAnswer { get; private set; }
        public string HeadAcheAnswer { get; private set; }
    }
}
