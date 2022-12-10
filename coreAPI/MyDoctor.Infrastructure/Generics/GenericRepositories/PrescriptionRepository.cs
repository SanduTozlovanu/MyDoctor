using AutoMapper;
using MyDoctorApp.Domain.Models;

namespace MyDoctorApp.Infrastructure.Generics.GenericRepositories
{
    public class PrescriptionRepository : Repository<Prescription>
    {
        public PrescriptionRepository(DatabaseContext context, IMapper mapper) : base(context, mapper) { }
    }
}
