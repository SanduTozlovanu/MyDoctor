using AutoMapper;
using MyDoctorApp.Domain.Models;

namespace MyDoctorApp.Infrastructure.Generics.GenericRepositories
{
    public class SurveyQuestionRepository : Repository<SurveyQuestion>
    {
        public SurveyQuestionRepository(DatabaseContext context, IMapper mapper) : base(context, mapper) { }
    }
}
