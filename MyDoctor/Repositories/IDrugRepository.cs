using MyDoctor.Bussiness.Entities;

namespace MyDoctor.Bussiness.Repositories
{
    public interface IDrugRepository
    {
        void Add(Drug drug);
        void Delete(int id);
        void Update(Drug drug);
    }
}