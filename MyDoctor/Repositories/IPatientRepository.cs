using MyDoctor.Bussiness.Entities;

namespace MyDoctor.Bussiness.Repositories
{
    public interface IPatientRepository
    {
        void Add(Patient patient);
        void Delete(int id);
        void Update(Patient patient);
    }
}
