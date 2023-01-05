using MediatR;
using MyDoctor.Application.Commands.SurveyQuestionsCommands;
using MyDoctor.Application.Mappers.SurveyQuestionsMappers;
using MyDoctor.Application.Response;
using MyDoctorApp.Domain.Models;
using MyDoctorApp.Infrastructure.Generics;
using System.Data.SqlTypes;

namespace MyDoctor.Application.Handlers.SurveyQuestionsHandlers
{
    public class UpdateSurveyQuestionsCommandHandler : IRequestHandler<UpdateSurveyQuestionsCommand, List<SurveyQuestionResponse>>
    {
        private readonly IRepository<SurveyQuestion> repository;

        public UpdateSurveyQuestionsCommandHandler(IRepository<SurveyQuestion> repository)
        {
            this.repository = repository;
        }
        public async Task<List<SurveyQuestionResponse>> Handle(UpdateSurveyQuestionsCommand request, CancellationToken cancellationToken)
        {
            List<SurveyQuestion>? surveyQuestionsEntityList = (await repository.FindAsync(sq => sq.PatientId == request.PatientId)).ToList();
            if (surveyQuestionsEntityList.Count == 0)
            {
                throw new SqlNullValueException();
            }
            for( int i = 0; i < surveyQuestionsEntityList.Count; i++)
            {
                for(int j = 0; j < request.QuestionList.Count; j++)
                {
                    if (surveyQuestionsEntityList[i].QuestionBody == request.QuestionList[j].QuestionBody)
                    {
                        surveyQuestionsEntityList[i].Update(request.QuestionList[j].Answer);
                    }
                }
                repository.Update(surveyQuestionsEntityList[i]);
            }
            await repository.SaveChangesAsync();
            return SurveyQuestionsMapper.Mapper.Map<List<SurveyQuestionResponse>>(surveyQuestionsEntityList);
        }
    }
}
