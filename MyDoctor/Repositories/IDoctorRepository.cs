using MyDoctor.Bussiness.Entities;

namespace MyDoctor.Bussiness.Repositories
{
    public interface IDoctorRepository
    {
        void Add(Doctor doctor);
        void Delete(int id);
        void Update(Doctor doctor);
    }
}