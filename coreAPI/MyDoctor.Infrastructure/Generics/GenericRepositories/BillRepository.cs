using MyDoctor.Domain.Models;

namespace MyDoctorApp.Infrastructure.Generics.GenericRepositories
{
    public class BillRepository : Repository<Bill>
    {
        public BillRepository(DatabaseContext context) : base(context) { }
    }
}
