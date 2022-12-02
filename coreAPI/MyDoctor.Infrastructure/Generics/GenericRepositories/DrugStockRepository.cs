using MyDoctor.Domain.Models;

namespace MyDoctorApp.Infrastructure.Generics.GenericRepositories
{
    public class DrugStockRepository : Repository<DrugStock>
    {
        public DrugStockRepository(DatabaseContext context) : base(context) { }
    }
}
