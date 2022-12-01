using MyDoctor.Domain.Models;

namespace MyDoctorApp.Infrastructure.Generics.GenericRepositories
{
    public class PrescriptedDrugRepository : Repository<PrescriptedDrug>
    {
        public PrescriptedDrugRepository(DatabaseContext context) : base(context) { }
    }
}
