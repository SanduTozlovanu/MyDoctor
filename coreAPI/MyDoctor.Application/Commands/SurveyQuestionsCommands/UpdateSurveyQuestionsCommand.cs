using MediatR;
using MyDoctor.Application.Responses;

namespace MyDoctor.Application.Commands.SurveyQuestionsCommands
{
    public class UpdateSurveyQuestionsCommand : IRequest<List<SurveyQuestionResponse>>
    {
        public UpdateSurveyQuestionsCommand(Guid patientId, List<SurveyQuestionResponse> questionList)
        {
            PatientId = patientId;
            QuestionList = questionList;
        }
        public Guid PatientId { get; private set; }
        public List<SurveyQuestionResponse> QuestionList { get; private set; }
    }
}
