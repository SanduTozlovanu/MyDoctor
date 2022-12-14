using Microsoft.EntityFrameworkCore;
using MyDoctorApp.Domain.Models;

namespace MyDoctorApp.Infrastructure
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options) { this.Database.EnsureCreated(); }
        public static string DatabaseName { get; set; } = "MyDoctorApp.db";
        public DbSet<Appointment> Appointments => Set<Appointment>();
        public DbSet<AppointmentInterval> AppointmentIntervals => Set<AppointmentInterval>();
        public DbSet<ScheduleInterval> ScheduleIntervals => Set<ScheduleInterval>();
        public DbSet<Bill> Bills => Set<Bill>();
        public DbSet<Doctor> Doctors => Set<Doctor>();
        public DbSet<Drug> Drugs => Set<Drug>();
        public DbSet<DrugStock> DrugStocks => Set<DrugStock>();
        public DbSet<MedicalHistory> MedicalHistories => Set<MedicalHistory>();
        public DbSet<MedicalRoom> MedicalRooms => Set<MedicalRoom>();
        public DbSet<Patient> Patients => Set<Patient>();
        public DbSet<Prescription> Prescriptions => Set<Prescription>();
        public DbSet<Procedure> Procedures => Set<Procedure>();
        public DbSet<PrescriptedDrug> PrescriptedDrugs => Set<PrescriptedDrug>();
        public DbSet<Speciality> Specialities => Set<Speciality>();

        public void Save()
        {
            SaveChanges();
        }

        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    optionsBuilder.UseSqlite($"Data Source = {DatabaseName}");
        //}

    }
}