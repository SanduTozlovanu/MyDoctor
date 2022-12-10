using AutoMapper;
using MyDoctorApp.Domain.Models;

namespace MyDoctorApp.Infrastructure.Generics.GenericRepositories
{
    public class PatientRepository : Repository<Patient>
    {
        public PatientRepository(DatabaseContext context, IMapper mapper) : base(context, mapper) { }
    }
}
