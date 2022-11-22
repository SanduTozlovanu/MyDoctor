using MyDoctor.Domain.Models;

namespace MyDoctorApp.Infrastructure.Generics.GenericRepositories
{
    public class MedicalHistoryRepository : Repository<MedicalHistory>
    {
        public MedicalHistoryRepository(DatabaseContext context) : base(context) { }
    }
}
