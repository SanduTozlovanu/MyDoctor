using MyDoctor.Domain.Models;

namespace MyDoctorApp.Infrastructure.Generics.GenericRepositories
{
    public class HospitalAdmissionFileRepository : Repository<HospitalAdmissionFile>
    {
        public HospitalAdmissionFileRepository(DatabaseContext context) : base(context) { }
    }
}
