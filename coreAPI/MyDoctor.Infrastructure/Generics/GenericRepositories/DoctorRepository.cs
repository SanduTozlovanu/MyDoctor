using AutoMapper;
using MyDoctorApp.Domain.Models;

namespace MyDoctorApp.Infrastructure.Generics.GenericRepositories
{
    public class DoctorRepository : Repository<Doctor>
    {
        public DoctorRepository(DatabaseContext context, IMapper mapper) : base(context, mapper) { }
    }
}
