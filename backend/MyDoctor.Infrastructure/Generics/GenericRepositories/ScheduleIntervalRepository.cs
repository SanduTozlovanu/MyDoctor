using MyDoctor;
using MyDoctor.Domain.Models;

namespace MyDoctorApp.Infrastructure.Generics.GenericRepositories
{
    public class ScheduleIntervalRepository : Repository<ScheduleInterval>
    {
        public ScheduleIntervalRepository(DatabaseContext context) : base(context) { }
    }
}
