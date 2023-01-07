using MediatR;
using Microsoft.AspNetCore.Mvc;
using MyDoctor.Application.Mappers.SurveyQuestionsMappers;
using MyDoctor.Application.Responses;
using MyDoctorApp.Domain.Models;
using MyDoctorApp.Infrastructure.Generics;

namespace MyDoctor.Application.Queries.SurveyQuestionsQueries
{
    public class GetPatientSurveyQuestionsQueryHandler :
        IRequestHandler<GetPatientSurveyQuestionsQuery,
            List<SurveyQuestionResponse>>
    {
        private const string PATIENT_NOTFOUND_ERROR = "Could not find a patient with this id";
        private readonly IRepository<SurveyQuestion> repository;

        public GetPatientSurveyQuestionsQueryHandler(IRepository<SurveyQuestion> repository)
        {
            this.repository = repository;
        }
        public async Task<List<SurveyQuestionResponse>> Handle(GetPatientSurveyQuestionsQuery request, CancellationToken cancellationToken)
        {
            var surveyQuestions = (await repository.FindAsync(sq => sq.PatientId == request.PatientId)).ToList();
            if (surveyQuestions == null)
            {
                var surveyQuestionsList = new List<SurveyQuestionResponse>();
                var surveyQuestionResponse = new SurveyQuestionResponse(string.Empty, string.Empty);
                surveyQuestionResponse.SetStatusResult(new NotFoundObjectResult(PATIENT_NOTFOUND_ERROR));
                surveyQuestionsList.Add(surveyQuestionResponse);
                return surveyQuestionsList;
            }
            return SurveyQuestionsMapper.Mapper.Map<List<SurveyQuestionResponse>>(surveyQuestions);
        }
    }
}
