﻿using AutoMapper;

namespace MyDoctor.Application.Mappers.DoctorAvailableAppointmentsMappers
{
    public static class DoctorAvailableAppointmentsMapper
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
                    cfg.AddProfile<DoctorAvailableAppointmentsProfile>();
                });
                var mapper = config.CreateMapper();
                return mapper;
            });
        public static IMapper Mapper => Lazy.Value;
    }
}