using AutoMapper;

namespace MyDoctor.Application.Mappers.DoctorAvailableAppointmentsMappers
{
    public static class DoctorAvailableAppointmentsMapper
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
                    cfg.AddProfile<DoctorAvailableAppointmentsProfile>();
                });
                var mapper = config.CreateMapper();
                return mapper;
            });
        public static IMapper Mapper => Lazy.Value;
    }
}
