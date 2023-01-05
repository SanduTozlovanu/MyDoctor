using MediatR;
using MyDoctor.Application.Commands.SurveyQuestionsCommands;
using MyDoctor.Application.Mappers.SurveyQuestionsMappers;
using MyDoctor.Application.Response;
using MyDoctorApp.Domain.Models;
using MyDoctorApp.Infrastructure.Generics;
using System.Data.SqlTypes;

namespace MyDoctor.Application.Handlers.SurveyQuestionsHandlers
{
    public class UpdateSurveyQuestionsCommandHandler : IRequestHandler<UpdateSurveyQuestionsCommand, SurveyQuestionsResponse>
    {
        private readonly IRepository<SurveyQuestions> repository;

        public UpdateSurveyQuestionsCommandHandler(IRepository<SurveyQuestions> repository)
        {
            this.repository = repository;
        }
        public async Task<SurveyQuestionsResponse> Handle(UpdateSurveyQuestionsCommand request, CancellationToken cancellationToken)
        {
            SurveyQuestions? surveyQuestionsEntity = (await repository.FindAsync(sq => sq.PatientId == request.PatientId)).FirstOrDefault();
            if (surveyQuestionsEntity == null)
            {
                throw new SqlNullValueException();
            }
            surveyQuestionsEntity.Update(request.CancerAnswer, request.BloodPressureAnswer,
                request.DiabetisAnswer, request.AllergiesAnswer, request.SexualAnswer, request.CovidAnswer, request.HeadAcheAnswer);
            var surveyQuestion = repository.Update(surveyQuestionsEntity);
            await repository.SaveChangesAsync();
            return SurveyQuestionsMapper.Mapper.Map<SurveyQuestionsResponse>(surveyQuestion);
        }
    }
}
