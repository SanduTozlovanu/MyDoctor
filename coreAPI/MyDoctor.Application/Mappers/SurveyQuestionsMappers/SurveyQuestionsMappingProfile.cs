using AutoMapper;
using MyDoctor.Application.Response;
using MyDoctorApp.Domain.Models;

namespace MyDoctor.Application.Mappers.SurveyQuestionsMappers
{
    public class SurveyQuestionsMappingProfile : Profile
    {
        public SurveyQuestionsMappingProfile()
        {
            CreateMap<SurveyQuestions, SurveyQuestionsResponse>().ReverseMap();
        }
    }
}
