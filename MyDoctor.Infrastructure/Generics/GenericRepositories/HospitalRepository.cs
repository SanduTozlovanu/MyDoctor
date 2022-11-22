using MyDoctor.Domain.Models;

namespace MyDoctorApp.Infrastructure.Generics.GenericRepositories
{
    public class HospitalRepository : Repository<Hospital>
    {
        public HospitalRepository(DatabaseContext context) : base(context) { }
    }
}
