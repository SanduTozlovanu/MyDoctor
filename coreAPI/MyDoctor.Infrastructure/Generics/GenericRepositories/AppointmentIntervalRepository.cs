using MyDoctor.Domain.Models;

namespace MyDoctorApp.Infrastructure.Generics.GenericRepositories
{
    public class AppointmentIntervalRepository : Repository<AppointmentInterval>
    {
        public AppointmentIntervalRepository(DatabaseContext context) : base(context) { }
    }
}
