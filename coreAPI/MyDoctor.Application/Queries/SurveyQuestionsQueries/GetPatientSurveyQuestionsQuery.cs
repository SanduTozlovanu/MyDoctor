using MediatR;
using MyDoctor.Application.Response;

namespace MyDoctor.Application.Queries.SurveyQuestionsQueries
{
    public class GetPatientSurveyQuestionsQuery : IRequest<List<SurveyQuestionResponse>>
    {
        public GetPatientSurveyQuestionsQuery(Guid patientId)
        {
            PatientId = patientId;
        }

        public Guid PatientId { get; private set; }
    }
}
