using MyDoctor.Bussiness.Entities;

namespace MyDoctor.Bussiness.Repositories
{
    public interface IAppointmentRepository
    {
        void Add(Appointment medicalHistory);
        void Delete(int id);
        void Update(Appointment medicalHistory);
    }
}
