using AutoMapper;
using MyDoctorApp.Domain.Models;

namespace MyDoctorApp.Infrastructure.Generics.GenericRepositories
{
    public class MedicalHistoryRepository : Repository<MedicalHistory>
    {
        public MedicalHistoryRepository(DatabaseContext context, IMapper mapper) : base(context, mapper) { }
    }
}
