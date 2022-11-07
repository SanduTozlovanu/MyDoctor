using MyDoctor.Bussiness.Entities;

namespace MyDoctor.Bussiness.Repositories
{
    public interface IMedicalHistoryRepository
    {
        void Add(MedicalHistory medicalHistory);
        void Delete(int id);
        void Update(MedicalHistory medicalHistory);
    }
}
