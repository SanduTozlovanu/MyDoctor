﻿using AutoMapper;

namespace MyDoctor.Application.Mappers.ScheduleIntervalMappers
{
    public static class ScheduleIntervalMapper
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
                    cfg.AddProfile<ScheduleIntervalMappingProfile>();
                });
                var mapper = config.CreateMapper();
                return mapper;
            });
        public static IMapper Mapper => Lazy.Value;
    }
}
