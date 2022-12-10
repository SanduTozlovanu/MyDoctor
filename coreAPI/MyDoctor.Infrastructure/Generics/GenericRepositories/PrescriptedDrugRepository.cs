using AutoMapper;
using MyDoctorApp.Domain.Models;

namespace MyDoctorApp.Infrastructure.Generics.GenericRepositories
{
    public class PrescriptedDrugRepository : Repository<PrescriptedDrug>
    {
        public PrescriptedDrugRepository(DatabaseContext context, IMapper mapper) : base(context, mapper) { }
    }
}