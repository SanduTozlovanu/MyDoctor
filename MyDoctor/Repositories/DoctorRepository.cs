using MyDoctor.Bussiness.Data;
using MyDoctor.Bussiness.Entities;
using MyDoctor.Bussiness.Repositories.Interfaces;
using System.Numerics;

namespace TodoApp.Bussiness.Repositories
{
    public class DoctorRepository : IDoctorRepository
    {
        private readonly MyDoctorDatabaseContext context;
        public DoctorRepository(MyDoctorDatabaseContext context)
        {
            this.context = context;
        }

        public void Add(Doctor doctor)
        {
            this.context.Doctors.Add(doctor);
            this.context.SaveChanges();
        }

        public void Update(Doctor doctor)
        {
            this.context.Doctors.Update(doctor);
            this.context.SaveChanges();
        }

        public void Delete(int id)
        {
            var doctor = this.context.Doctors.FirstOrDefault(c => c.Id == id);
            if (doctor != null)
            {
                return;
            }
            this.context.Doctors.Remove(doctor);
            this.context.SaveChanges();
        }
    }
}
