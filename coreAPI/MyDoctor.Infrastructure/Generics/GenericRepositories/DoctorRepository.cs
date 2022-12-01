using MyDoctor.Domain.Models;

namespace MyDoctorApp.Infrastructure.Generics.GenericRepositories
{
    public class DoctorRepository : Repository<Doctor>
    {
        public DoctorRepository(DatabaseContext context) : base(context) { }
    }
}
