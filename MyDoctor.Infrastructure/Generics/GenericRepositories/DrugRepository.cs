using MyDoctor.Domain.Models;

namespace MyDoctorApp.Infrastructure.Generics.GenericRepositories
{
    public class DrugRepository : Repository<Drug>
    {
        public DrugRepository(DatabaseContext context) : base(context) { }
    }
}
