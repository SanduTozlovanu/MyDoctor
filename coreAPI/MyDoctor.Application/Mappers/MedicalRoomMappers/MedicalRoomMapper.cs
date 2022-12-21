using AutoMapper;

namespace MyDoctor.Application.Mappers.MedicalRoomMappers
{
    public static class AvailableAppointmentIntervalsMapper
    {
        private static Lazy<IMapper> Lazy =
            new Lazy<IMapper>(() =>
            {
                var config = new MapperConfiguration(cfg =>
                {
                    cfg.ShouldMapProperty = p =>
                    {
                        if (p.GetMethod == null) return false;
                        return p.GetMethod.IsPublic ||
                        p.GetMethod.IsAssembly;
                    };
                    cfg.AddProfile<AvailableAppointmentIntervalsProfile>();
                });
                var mapper = config.CreateMapper();
                return mapper;
            });
        public static IMapper Mapper => Lazy.Value;
    }
}
