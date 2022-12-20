using AutoMapper;
using MyDoctor.Application.Commands.MedicalRoomCommands;
using MyDoctor.Application.Responses;
using MyDoctorApp.Domain.Models;

namespace MyDoctor.Application.Mappers.MedicalRoomMappers
{
    public class AvailableAppointmentIntervalsProfile : Profile
    {
        public AvailableAppointmentIntervalsProfile()
        {
            CreateMap<MedicalRoom, MedicalRoomResponse>().ReverseMap();
            CreateMap<MedicalRoom, CreateMedicalRoomCommand>().ReverseMap();
        }
    }
}
