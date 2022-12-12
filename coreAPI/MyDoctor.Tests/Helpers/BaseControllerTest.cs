using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using MyDoctorApp.Infrastructure;

namespace MyDoctor.Tests.Helpers
{
    [TestCaseOrderer("MyDoctor.Tests.Orderers.PriorityOrderer", "MyDoctor.Tests")]
    public class BaseControllerTest<T> : IClassFixture<CustomWebApplicationFactory<Program>> where T: class
    {
        protected CustomWebApplicationFactory<Program> Factory { get; }
        public DatabaseContext DatabaseContext { get; private set; }

        protected readonly HttpClient _client;

        // Ctor is called for every test method
        public BaseControllerTest()
        {
            Factory = new CustomWebApplicationFactory<Program>();
            _client = Factory.CreateClient();
            var scope = Factory.Services.GetService<IServiceScopeFactory>().CreateScope();
            DatabaseContext = scope.ServiceProvider.GetService<DatabaseContext>();
            CleanDatabases();
        }

        private void CleanDatabases()
        {
            using var scope = Factory.Services.CreateScope();
            var scopedServices = scope.ServiceProvider;
            var databaseContext = DatabaseContext;

            // Clean DataBase
            databaseContext.Patients.RemoveRange(databaseContext.Patients.ToList());
            databaseContext.Doctors.RemoveRange(databaseContext.Doctors.ToList());
            databaseContext.MedicalRooms.RemoveRange(databaseContext.MedicalRooms.ToList());
            databaseContext.MedicalHistories.RemoveRange(databaseContext.MedicalHistories.ToList());
            databaseContext.Drugs.RemoveRange(databaseContext.Drugs.ToList());
            databaseContext.DrugStocks.RemoveRange(databaseContext.DrugStocks.ToList());
            databaseContext.PrescriptedDrugs.RemoveRange(databaseContext.PrescriptedDrugs.ToList());
            databaseContext.DrugStocks.RemoveRange(databaseContext.DrugStocks.ToList());
            databaseContext.Appointments.RemoveRange(databaseContext.Appointments.ToList());
            databaseContext.AppointmentIntervals.RemoveRange(databaseContext.AppointmentIntervals.ToList());
            databaseContext.Bills.RemoveRange(databaseContext.Bills.ToList());
            databaseContext.Prescriptions.RemoveRange(databaseContext.Prescriptions.ToList());
            databaseContext.Procedures.RemoveRange(databaseContext.Procedures.ToList());
            databaseContext.ScheduleIntervals.RemoveRange(databaseContext.ScheduleIntervals.ToList());
            databaseContext.SaveChangesAsync();
        }
    }
}