using MyDoctor.Domain.Models;

namespace MyDoctorApp.Infrastructure.Generics.GenericRepositories
{
    public class AppointmentRepository : Repository<Appointment>
    {
        public AppointmentRepository(DatabaseContext context) : base(context) { }
    }
}
