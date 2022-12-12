using MyDoctorApp.Infrastructure;
namespace MyDoctor.Tests.Helpers
{
    public class DatabaseFixtureGeneric<T> : IDisposable where T : class
    {
        public DatabaseFixtureGeneric()
        {
            DatabaseContext = new DatabaseContext();
            DatabaseContext.DatabaseName = $"{typeof(T).Name}_Test.db";
            DatabaseContext.Database.EnsureCreated();
            Dispose();
        }

        public DatabaseContext DatabaseContext { get; private set; }

        public void Dispose()
        {
            // Clean DataBase
            DatabaseContext.Patients.RemoveRange(DatabaseContext.Patients.ToList());
            DatabaseContext.Doctors.RemoveRange(DatabaseContext.Doctors.ToList());
            DatabaseContext.MedicalRooms.RemoveRange(DatabaseContext.MedicalRooms.ToList());
            DatabaseContext.MedicalHistories.RemoveRange(DatabaseContext.MedicalHistories.ToList());
            DatabaseContext.Drugs.RemoveRange(DatabaseContext.Drugs.ToList());
            DatabaseContext.DrugStocks.RemoveRange(DatabaseContext.DrugStocks.ToList());
            DatabaseContext.PrescriptedDrugs.RemoveRange(DatabaseContext.PrescriptedDrugs.ToList());
            DatabaseContext.DrugStocks.RemoveRange(DatabaseContext.DrugStocks.ToList());
            DatabaseContext.Appointments.RemoveRange(DatabaseContext.Appointments.ToList());
            DatabaseContext.AppointmentIntervals.RemoveRange(DatabaseContext.AppointmentIntervals.ToList());
            DatabaseContext.Bills.RemoveRange(DatabaseContext.Bills.ToList());
            DatabaseContext.Prescriptions.RemoveRange(DatabaseContext.Prescriptions.ToList());
            DatabaseContext.Procedures.RemoveRange(DatabaseContext.Procedures.ToList());
            DatabaseContext.ScheduleIntervals.RemoveRange(DatabaseContext.ScheduleIntervals.ToList());
            DatabaseContext.SaveChangesAsync();
        }
    }
}