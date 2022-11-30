using MyDoctor.Domain.Models;

namespace MyDoctorApp.Infrastructure.Generics.GenericRepositories
{
    public class ProcedureRepository : Repository<Procedure>
    {
        public ProcedureRepository(DatabaseContext context) : base(context) { }
    }
}
