using MediatR;
using MyDoctor.Application.Mappers.SurveyQuestionsMappers;
using MyDoctor.Application.Queries.SurveyQuestionsQueries;
using MyDoctor.Application.Response;
using MyDoctorApp.Domain.Models;
using MyDoctorApp.Infrastructure.Generics;
using System.Data.SqlTypes;

namespace MyDoctor.Application.Queries.GetPatientSurveyQuestionsQueryHandler
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
            if(surveyQuestions == null)
            {
                throw new SqlNullValueException();
            }
            return SurveyQuestionsMapper.Mapper.Map<List<SurveyQuestionResponse>>(surveyQuestions);
        }
    }
}
