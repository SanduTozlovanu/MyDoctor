using AutoMapper;
using MyDoctorApp.Domain.Models;

namespace MyDoctorApp.Infrastructure.Generics.GenericRepositories
{
    public class ProcedureRepository : Repository<Procedure>
    {
        public ProcedureRepository(DatabaseContext context, IMapper mapper) : base(context, mapper) { }
    }
}
