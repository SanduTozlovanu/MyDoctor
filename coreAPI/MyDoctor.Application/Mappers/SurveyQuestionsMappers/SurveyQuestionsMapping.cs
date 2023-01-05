using AutoMapper;

namespace MyDoctor.Application.Mappers.SurveyQuestionsMappers
{
    public static class SurveyQuestionsMapper
    {
        private static Lazy<IMapper> Lazy =
            new(() =>
            {
                var config = new MapperConfiguration(cfg =>
                {
                    cfg.ShouldMapProperty = p =>
                    {
                        return p.GetMethod == null
                            ? false
                            : p.GetMethod.IsPublic ||
                        p.GetMethod.IsAssembly;
                    };
                    cfg.AddProfile<SurveyQuestionsMappingProfile>();
                });
                var mapper = config.CreateMapper();
                return mapper;
            });
        public static IMapper Mapper => Lazy.Value;
    }
}
