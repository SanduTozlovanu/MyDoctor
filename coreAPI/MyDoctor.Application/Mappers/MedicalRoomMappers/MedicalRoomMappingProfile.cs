using AutoMapper;
using MyDoctor.Application.Commands.MedicalRoomCommands;
using MyDoctor.Application.Responses;
using MyDoctorApp.Domain.Models;

namespace MyDoctor.Application.Mappers.MedicalRoomMappers
{
    public class MedicalRoomMappingProfile : Profile
    {
        public MedicalRoomMappingProfile()
        {
            CreateMap<MedicalRoom, MedicalRoomResponse>().ReverseMap();
            CreateMap<MedicalRoom, CreateMedicalRoomCommand>().ReverseMap();
        }
    }
}
