using AutoMapper;
using MyDoctorApp.Domain.Models;

namespace MyDoctorApp.Infrastructure.Generics.GenericRepositories
{
    public class AppointmentRepository : Repository<Appointment>
    {
        public AppointmentRepository(DatabaseContext context, IMapper mapper) : base(context, mapper) { }
    }
}
