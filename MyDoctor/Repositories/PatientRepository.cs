using MyDoctor.Bussiness.Data;
using MyDoctor.Bussiness.Entities;
using MyDoctor.Bussiness.Repositories.Interfaces;

namespace MyDoctor.Bussiness.Repositories
{
    public class PatientRepository : IPatientRepository
    {
        private readonly MyDoctorDatabaseContext context;
        public PatientRepository(MyDoctorDatabaseContext context)
        {
            this.context = context;
        }

        public void Add(Patient patient)
        {
            this.context.Patients.Add(patient);
            this.context.SaveChanges();
        }

        public void Update(Patient patient)
        {
            this.context.Patients.Update(patient);
            this.context.SaveChanges();
        }

        public void Delete(int id)
        {
            var patient = this.context.Patients.FirstOrDefault(c => c.Id == id);
            if (patient != null)
            {
                return;
            }
            this.context.Patients.Remove(patient);
            this.context.SaveChanges();
        }
    }
}
