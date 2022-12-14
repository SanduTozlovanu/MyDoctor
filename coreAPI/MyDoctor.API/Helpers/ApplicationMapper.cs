using AutoMapper;
using MyDoctor.API.DTOs;
using MyDoctorApp.Domain.Models;

namespace MyDoctor.API.Helpers
{
    public class ApplicationMapper : Profile
    {
        public ApplicationMapper()
        {
            CreateMap<MedicalRoom, DisplayMedicalRoomDto>().ReverseMap();
            CreateMap<Doctor, DisplayDoctorDto>();
            CreateMap<Patient, DisplayPatientDto>();
            CreateMap<DrugStock, DisplayDrugStockDto>().ReverseMap();
            CreateMap<PrescriptedDrug, DisplayPrescriptedDrugDto>().ReverseMap();
            CreateMap<User, DisplayUserDto>().ReverseMap();
            CreateMap<Procedure, DisplayProcedureDto>().ReverseMap();
            CreateMap<Prescription, DisplayPrescriptionDto>().ReverseMap();
            CreateMap<Bill, DisplayBillDto>().ReverseMap();
            CreateMap<MedicalHistory, DisplayMedicalHistoryDto>().ReverseMap();
            CreateMap<Appointment, DisplayAppointmentDto>().ReverseMap();
            CreateMap<Drug, DisplayDrugDto>().ReverseMap();
            CreateMap<Speciality, DisplaySpecialityDto>().ReverseMap();
            CreateMap<Drug, GetDrugDto>();

            CreateMap<CreateAppointmentDto, Appointment>();
            CreateMap<CreatePrescriptionDto, Prescription>();
            CreateMap<CreateProcedureDto, Procedure>();
            CreateMap<CreateDoctorDto, Doctor>();
            CreateMap<CreatePatientDto, Patient>();
            CreateMap<CreateDrugDto, Drug>();
            CreateMap<CreatePatientDto, Patient>();
            CreateMap<CreateMedicalRoomDto, MedicalRoom>();
            CreateMap<CreateSpecialityDto, Speciality>();


        }
    }
}
