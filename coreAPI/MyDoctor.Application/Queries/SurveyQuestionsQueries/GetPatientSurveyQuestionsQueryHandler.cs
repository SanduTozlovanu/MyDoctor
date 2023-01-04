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
            SurveyQuestionsResponse>
    {
        private readonly IRepository<SurveyQuestions> repository;

        public GetPatientSurveyQuestionsQueryHandler(IRepository<SurveyQuestions> repository)
        {
            this.repository = repository;
        }
        public async Task<SurveyQuestionsResponse> Handle(GetPatientSurveyQuestionsQuery request, CancellationToken cancellationToken)
        {
            var surveyQuestions = (await repository.FindAsync(sq => sq.PatientId == request.PatientId)).FirstOrDefault();
            if (surveyQuestions == null) 
            {
                throw new SqlNullValueException();
            }
            return SurveyQuestionsMapper.Mapper.Map<SurveyQuestionsResponse>(surveyQuestions);
        }
    }
}
