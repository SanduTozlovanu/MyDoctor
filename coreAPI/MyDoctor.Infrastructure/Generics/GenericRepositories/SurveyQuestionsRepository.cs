using AutoMapper;
using MyDoctorApp.Domain.Models;

namespace MyDoctorApp.Infrastructure.Generics.GenericRepositories
{
    public class SurveyQuestionsRepository : Repository<SurveyQuestions>
    {
        public SurveyQuestionsRepository(DatabaseContext context, IMapper mapper) : base(context, mapper) { }
    }
}
