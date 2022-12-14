using AutoMapper;
using MyDoctorApp.Domain.Models;

namespace MyDoctorApp.Infrastructure.Generics.GenericRepositories
{
    public class SpecialityRepository : Repository<Speciality>
    {
        public SpecialityRepository(DatabaseContext context, IMapper mapper) : base(context, mapper) { }
    }
}
