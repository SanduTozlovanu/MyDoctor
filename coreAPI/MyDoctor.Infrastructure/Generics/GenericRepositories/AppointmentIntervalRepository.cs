using AutoMapper;
using MyDoctorApp.Domain.Models;

namespace MyDoctorApp.Infrastructure.Generics.GenericRepositories
{
    public class AppointmentIntervalRepository : Repository<AppointmentInterval>
    {
        public AppointmentIntervalRepository(DatabaseContext context, IMapper mapper) : base(context, mapper) { }
    }
}
