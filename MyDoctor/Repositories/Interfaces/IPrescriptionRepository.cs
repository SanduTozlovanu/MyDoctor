using MyDoctor.Bussiness.Entities;

namespace MyDoctor.Bussiness.Repositories.Interfaces
{
    public interface IPrescriptionRepository
    {
        void Add(Prescription prescription);
        void Delete(int id);
        void Update(Prescription prescription);
    }
}