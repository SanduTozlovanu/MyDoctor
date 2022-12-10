using AutoMapper;
using MyDoctorApp.Domain.Models;

namespace MyDoctorApp.Infrastructure.Generics.GenericRepositories
{
    public class DrugRepository : Repository<Drug>
    {
        public DrugRepository(DatabaseContext context, IMapper mapper) : base(context, mapper) { }
    }
}
