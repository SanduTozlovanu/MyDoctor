using Microsoft.EntityFrameworkCore;
using MyDoctor;
using MyDoctor.Domain.Models;

namespace MyDoctorApp.Infrastructure
{
    public class DatabaseContext : DbContext
    {
        public DbSet<Appointment> Appointments => Set<Appointment>();
        public DbSet<AppointmentInterval> AppointmentIntervals => Set<AppointmentInterval>();
        public DbSet<Bill> bills => Set<Bill>();
        public DbSet<Doctor> Doctors => Set<Doctor>();
        public DbSet<Drug> Drugs => Set<Drug>();
        public DbSet<DrugStock> DrugStocks => Set<DrugStock>();
        public DbSet<Hospital> Hospitals => Set<Hospital>();
        public DbSet<HospitalAdmissionFile> HospitalAdmissionFiles => Set<HospitalAdmissionFile>();
        public DbSet<MedicalHistory> MedicalHistories => Set<MedicalHistory>();
        public DbSet<MedicalRoom> MedicalRooms => Set<MedicalRoom>();
        public DbSet<Patient> Patients => Set<Patient>();
        public DbSet<Prescription> Prescriptions => Set<Prescription>();
        public DbSet<Procedure> Procedures => Set<Procedure>();

        public void Save()
        {
            SaveChanges();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source = MyDoctorApp.db");
        }
    }
}