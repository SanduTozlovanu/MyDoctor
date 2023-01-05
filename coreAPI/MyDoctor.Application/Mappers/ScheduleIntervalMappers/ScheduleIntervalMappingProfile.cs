using AutoMapper;
using MyDoctor.Application.Response;
using MyDoctorApp.Domain.Models;

namespace MyDoctor.Application.Mappers.ScheduleIntervalMappers
{
    public class ScheduleIntervalMappingProfile : Profile
    {
        public ScheduleIntervalMappingProfile()
        {
            CreateMap<ScheduleInterval, ScheduleIntervalResponse>().ReverseMap();
        }
    }
}
