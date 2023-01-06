using AutoMapper;
using MyDoctor.Application.Responses;
using MyDoctorApp.Domain.Models;

namespace MyDoctor.Application.Mappers.SurveyQuestionsMappers
{
    public class SurveyQuestionsMappingProfile : Profile
    {
        public SurveyQuestionsMappingProfile()
        {
            CreateMap<SurveyQuestion, SurveyQuestionResponse>().ReverseMap();
        }
    }
}
