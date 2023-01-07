using AutoMapper;

namespace MyDoctor.Application.Mappers.MedicalRoomMappers
{
    public static class MedicalRoomMapper
    {
        private static readonly Lazy<IMapper> Lazy =
            new(() =>
            {
                var config = new MapperConfiguration(cfg =>
                {
                    cfg.ShouldMapProperty = p =>
                    {
                        return p.GetMethod != null
                        && (p.GetMethod.IsPublic ||
                        p.GetMethod.IsAssembly);
                    };
                    cfg.AddProfile<MedicalRoomMappingProfile>();
                });
                var mapper = config.CreateMapper();
                return mapper;
            });
        public static IMapper Mapper => Lazy.Value;
    }
}
