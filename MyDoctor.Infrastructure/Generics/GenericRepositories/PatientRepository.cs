using MyDoctor.Domain.Models;

namespace MyDoctorApp.Infrastructure.Generics.GenericRepositories
{
    public class PatientRepository : Repository<Patient>
    {
        public PatientRepository(DatabaseContext context) : base(context) { }
    }
}
