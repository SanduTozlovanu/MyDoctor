using MyDoctor.Domain.Models;

namespace MyDoctorApp.Infrastructure.Generics.GenericRepositories
{
    public class PrescriptionRepository : Repository<Prescription>
    {
        public PrescriptionRepository(DatabaseContext context) : base(context) { }
    }
}
