using AutoMapper;
using MyDoctor.Application.Commands.MedicalRoomCommands;
using MyDoctor.Application.Response;
using MyDoctor.Application.Responses;
using MyDoctorApp.Domain.Models;

namespace MyDoctor.Application.Mappers.AvailableAppointmentIntervalsMappers
{
    public class AvailableAppointmentIntervalsProfile : Profile
    {
        public AvailableAppointmentIntervalsProfile()
        {
            CreateMap<Tuple<TimeOnly, TimeOnly>, AvailableAppointmentIntervalsResponse>().ReverseMap();
        }
    }
}
