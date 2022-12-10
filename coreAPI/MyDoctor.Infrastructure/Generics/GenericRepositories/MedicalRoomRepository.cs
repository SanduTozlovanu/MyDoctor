using AutoMapper;
using MyDoctorApp.Domain.Models;

namespace MyDoctorApp.Infrastructure.Generics.GenericRepositories
{
    public class MedicalRoomRepository : Repository<MedicalRoom>
    {
        public MedicalRoomRepository(DatabaseContext context, IMapper mapper) : base(context, mapper) { }
    }
}
