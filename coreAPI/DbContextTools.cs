using System;

public class DbContextTools
{
    public static DatabaseContext CreateDbContext()
    {
        var dbContext = new DatabaseContext();
        DatabaseContext.DatabaseName = "Tests.db";
        dbContext.Database.EnsureCreated();

        // Clean DataBase
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
        dbContext.SaveChanges();

        return dbContext;
    }
}
