using MyDoctor.Bussiness.Data;
using MyDoctor.Bussiness.Entities;
using MyDoctor.Bussiness.Repositories;

namespace TodoApp.Bussiness.Repositories
{
    public class HospitalRepository : IHospitalRepository
    {
        private readonly MyDoctorDatabaseContext context;
        public HospitalRepository(MyDoctorDatabaseContext context)
        {
            this.context = context;
        }

        public void Add(Hospital hospital)
        {
            this.context.Hospitals.Add(hospital);
            this.context.SaveChanges();
        }

        public void Update(Hospital hospital)
        {
            this.context.Hospitals.Update(hospital);
            this.context.SaveChanges();
        }

        public void Delete(int id)
        {
            var hospital = this.context.Hospitals.FirstOrDefault(c => c.Id == id);
            if (hospital != null)
            {
                return;
            }
            this.context.Hospitals.Remove(hospital);
            this.context.SaveChanges();
        }
    }
}
