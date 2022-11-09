using MyDoctor.Bussiness.Data;
using MyDoctor.Bussiness.Entities;
using MyDoctor.Bussiness.Repositories.Interfaces;

namespace MyDoctor.Bussiness.Repositories
{
    public class PrescriptionRepository : IPrescriptionRepository
    {
        private readonly MyDoctorDatabaseContext context;
        public PrescriptionRepository(MyDoctorDatabaseContext context)
        {
            this.context = context;
        }

        public void Add(Prescription prescription)
        {
            this.context.Prescriptions.Add(prescription);
            this.context.SaveChanges();
        }

        public void Update(Prescription prescription)
        {
            this.context.Prescriptions.Update(prescription);
            this.context.SaveChanges();
        }

        public void Delete(int id)
        {
            var prescription = this.context.Prescriptions.FirstOrDefault(c => c.Id == id);
            if (prescription != null)
            {
                return;
            }
            this.context.Prescriptions.Remove(prescription);
            this.context.SaveChanges();
        }
    }
}
