using AutoMapper;
using MyDoctor.Application.Response;

namespace MyDoctor.Application.Mappers.DoctorAvailableAppointmentsMappers
{
    public class DoctorAvailableAppointmentsProfile : Profile
    {
        public DoctorAvailableAppointmentsProfile()
        {
            CreateMap<Tuple<TimeOnly, TimeOnly>, IntervalResponse>()
            .ForMember(dest =>
                dest.StartTime,
                opt => opt.MapFrom(src => src.Item1.ToString("HH:mm")))
            .ForMember(dest =>
                dest.EndTime,
                opt => opt.MapFrom(src => src.Item2.ToString("HH:mm")))
            .ReverseMap();
        }
    }
}
