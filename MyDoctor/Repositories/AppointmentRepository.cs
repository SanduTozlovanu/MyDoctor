using MyDoctor.Bussiness.Data;
using MyDoctor.Bussiness.Entities;

namespace MyDoctor.Bussiness.Repositories
{
    public class AppointmentRepository : IAppointmentRepository
    {
        private readonly MyDoctorDatabaseContext context;
        public AppointmentRepository(MyDoctorDatabaseContext context)
        {
            this.context = context;
        }

        public void Add(Appointment appointment)
        {
            this.context.Appointments.Add(appointment);
            this.context.SaveChanges();
        }

        public void Update(Appointment appointment)
        {
            this.context.Appointments.Update(appointment);
            this.context.SaveChanges();
        }

        public void Delete(int id)
        {
            var appointment = this.context.Appointments.FirstOrDefault(c => c.Id == id);
            if (appointment != null)
            {
                return;
            }
            this.context.Appointments.Remove(appointment);
            this.context.SaveChanges();
        }
    }
}