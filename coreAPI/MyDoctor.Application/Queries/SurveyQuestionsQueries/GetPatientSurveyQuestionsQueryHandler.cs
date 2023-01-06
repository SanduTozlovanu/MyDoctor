using MediatR;
using MyDoctor.Application.Mappers.SurveyQuestionsMappers;
using MyDoctor.Application.Responses;
using MyDoctorApp.Domain.Models;
using MyDoctorApp.Infrastructure.Generics;
using System.Data.SqlTypes;

namespace MyDoctor.Application.Queries.SurveyQuestionsQueries
{
    public class GetPatientSurveyQuestionsQueryHandler :
        IRequestHandler<GetPatientSurveyQuestionsQuery,
            List<SurveyQuestionResponse>>
    {
        private readonly IRepository<SurveyQuestion> repository;

        public GetPatientSurveyQuestionsQueryHandler(IRepository<SurveyQuestion> repository)
        {
            this.repository = repository;
        }
        public async Task<List<SurveyQuestionResponse>> Handle(GetPatientSurveyQuestionsQuery request, CancellationToken cancellationToken)
        {
            var surveyQuestions = (await repository.FindAsync(sq => sq.PatientId == request.PatientId)).ToList();
            return surveyQuestions == null
                ? throw new SqlNullValueException()
                : SurveyQuestionsMapper.Mapper.Map<List<SurveyQuestionResponse>>(surveyQuestions);
        }
    }
}
