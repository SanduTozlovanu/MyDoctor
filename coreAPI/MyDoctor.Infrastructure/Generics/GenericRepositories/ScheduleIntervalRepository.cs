using AutoMapper;
using MyDoctorApp.Domain.Models;

namespace MyDoctorApp.Infrastructure.Generics.GenericRepositories
{
    public class ScheduleIntervalRepository : Repository<ScheduleInterval>
    {
        public ScheduleIntervalRepository(DatabaseContext context, IMapper mapper) : base(context, mapper) { }
    }
}
