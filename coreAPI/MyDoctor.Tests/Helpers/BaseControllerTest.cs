using Microsoft.Extensions.DependencyInjection;
using MyDoctorApp.Infrastructure;

namespace MyDoctor.Tests.Helpers
{
    [TestCaseOrderer("MyDoctor.Tests.Orderers.PriorityOrderer", "MyDoctor.Tests")]
    public class BaseControllerTest<T> : IClassFixture<CustomWebApplicationFactory<Program>> where T: class
    {
        protected readonly HttpClient HttpClient;
        protected CustomWebApplicationFactory<Program> Factory = new CustomWebApplicationFactory<Program>();

        // Ctor is called for every test method
        public BaseControllerTest(CustomWebApplicationFactory<Program> factory)
        {
            Factory = factory;
            HttpClient = factory.CreateClient();
            CleanDatabase();
        }

        private void CleanDatabase()
        {
            using (var scope = Factory.Services.CreateScope())
            {
                var scopedServices = scope.ServiceProvider;
                var dbContext = scopedServices.GetRequiredService<DatabaseContext>();

                dbContext.Patients.RemoveRange(dbContext.Patients.ToList());
                dbContext.Doctors.RemoveRange(dbContext.Doctors.ToList());
                dbContext.MedicalRooms.RemoveRange(dbContext.MedicalRooms.ToList());
                dbContext.MedicalHistories.RemoveRange(dbContext.MedicalHistories.ToList());
                dbContext.Drugs.RemoveRange(dbContext.Drugs.ToList());
                dbContext.DrugStocks.RemoveRange(dbContext.DrugStocks.ToList());
                dbContext.PrescriptedDrugs.RemoveRange(dbContext.PrescriptedDrugs.ToList());
                dbContext.DrugStocks.RemoveRange(dbContext.DrugStocks.ToList());
                dbContext.Appointments.RemoveRange(dbContext.Appointments.ToList());
                dbContext.AppointmentIntervals.RemoveRange(dbContext.AppointmentIntervals.ToList());
                dbContext.Bills.RemoveRange(dbContext.Bills.ToList());
                dbContext.Prescriptions.RemoveRange(dbContext.Prescriptions.ToList());
                dbContext.Procedures.RemoveRange(dbContext.Procedures.ToList());
                dbContext.ScheduleIntervals.RemoveRange(dbContext.ScheduleIntervals.ToList());
                dbContext.SaveChangesAsync();
            }
        }
    }
}