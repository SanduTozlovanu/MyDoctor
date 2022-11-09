using MyDoctor.Bussiness.Data;
using MyDoctor.Bussiness.Entities;
using MyDoctor.Bussiness.Repositories.Interfaces;

namespace MyDoctor.Bussiness.Repositories
{
    public class MedicalHistoryRepository : IMedicalHistoryRepository
    {
        private readonly MyDoctorDatabaseContext context;
        public MedicalHistoryRepository(MyDoctorDatabaseContext context)
        {
            this.context = context;
        }

        public void Add(MedicalHistory medicalHistory)
        {
            this.context.MedicalHistories.Add(medicalHistory);
            this.context.SaveChanges();
        }

        public void Update(MedicalHistory medicalHistory)
        {
            this.context.MedicalHistories.Update(medicalHistory);
            this.context.SaveChanges();
        }

        public void Delete(int id)
        {
            var medicalHistory = this.context.MedicalHistories.FirstOrDefault(c => c.Id == id);
            if (medicalHistory != null)
            {
                return;
            }
            this.context.MedicalHistories.Remove(medicalHistory);
            this.context.SaveChanges();
        }
    }
}
