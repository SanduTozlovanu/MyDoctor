using MediatR;
using Microsoft.AspNetCore.Mvc;
using MyDoctor.Application.Commands.SurveyQuestionsCommands;
using MyDoctor.Application.Mappers.SurveyQuestionsMappers;
using MyDoctor.Application.Responses;
using MyDoctorApp.Domain.Models;
using MyDoctorApp.Infrastructure.Generics;

namespace MyDoctor.Application.Handlers.SurveyQuestionsHandlers
{
    public class UpdateSurveyQuestionsCommandHandler : IRequestHandler<UpdateSurveyQuestionsCommand, List<SurveyQuestionResponse>>
    {
        private const string SURVEYQUESTIONS_NOTFOUND_ERROR = "The list of survey Questions for this patient id could not be found!";
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
                var responseList = new List<SurveyQuestionResponse>();
                var surveyResponse = new SurveyQuestionResponse(string.Empty, string.Empty);
                surveyResponse.SetStatusResult(new NotFoundObjectResult(SURVEYQUESTIONS_NOTFOUND_ERROR));
                responseList.Add(surveyResponse);
                return responseList;
            }
            for (int i = 0; i < surveyQuestionsEntityList.Count; i++)
            {
                for (int j = 0; j < request.QuestionList.Count; j++)
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
