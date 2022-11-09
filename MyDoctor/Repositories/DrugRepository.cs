using MyDoctor.Bussiness.Data;
using MyDoctor.Bussiness.Entities;
using MyDoctor.Bussiness.Repositories.Interfaces;

namespace MyDoctor.Bussiness.Repositories
{
    public class DrugRepository : IDrugRepository
    {
        private readonly MyDoctorDatabaseContext context;
        public DrugRepository(MyDoctorDatabaseContext context)
        {
            this.context = context;
        }

        public void Add(Drug drug)
        {
            this.context.Drugs.Add(drug);
            this.context.SaveChanges();
        }

        public void Update(Drug drug)
        {
            this.context.Drugs.Update(drug);
            this.context.SaveChanges();
        }

        public void Delete(int id)
        {
            var drug = this.context.Drugs.FirstOrDefault(c => c.Id == id);
            if (drug != null)
            {
                return;
            }
            this.context.Drugs.Remove(drug);
            this.context.SaveChanges();
        }
    }
}
