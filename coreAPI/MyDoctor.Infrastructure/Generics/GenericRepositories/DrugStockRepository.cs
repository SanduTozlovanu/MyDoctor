using AutoMapper;
using MyDoctorApp.Domain.Models;

namespace MyDoctorApp.Infrastructure.Generics.GenericRepositories
{
    public class DrugStockRepository : Repository<DrugStock>
    {
        public DrugStockRepository(DatabaseContext context, IMapper mapper) : base(context, mapper) { }
    }
}
