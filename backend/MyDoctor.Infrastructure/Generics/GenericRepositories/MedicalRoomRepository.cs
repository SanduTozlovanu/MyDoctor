using MyDoctor.Domain.Models;

namespace MyDoctorApp.Infrastructure.Generics.GenericRepositories
{
    public class MedicalRoomRepository : Repository<MedicalRoom>
    {
        public MedicalRoomRepository(DatabaseContext context) : base(context) { }
    }
}
