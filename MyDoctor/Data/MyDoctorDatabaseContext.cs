using Microsoft.EntityFrameworkCore;
using MyDoctor.Bussiness.Entities;
using System.Collections.Generic;
using System.Reflection.Emit;

namespace MyDoctor.Bussiness.Data
{
    public class MyDoctorDatabaseContext : DbContext
    {
        public DbSet<Appointment> Appointments { get; set; }
        public DbSet<Doctor> Doctors { get; set; }
        public DbSet<Drug> Drugs { get; set; }
        public DbSet<Hospital> Hospitals { get; set; }
        public DbSet<MedicalHistory> MedicalHistories { get; set; }
        public DbSet<Patient> Patients { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source = MyDoctorManagement.db");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var drug1 = new Drug
            {
                Id = 1,
                Name = "Paracetamol",
                Description = "Paracetamol description",
                Price = 20, 
                Quantity = 574996
            };

            var drug2 = new Drug
            {
                Id = 2,
                Name = "Ibuprofen",
                Description = "Ibuprofen description",
                Price = 29,
                Quantity = 254652
            };
            var patient1 = new Patient
            {
                Id = 1,
                Mail = "user@gmail.com",
                Password = "akmfd795",
                FirstName = "Ion",
                LastName = "Raluca",
                Age = 56,           
            };

            var patient2 = new Patient
            {
                Id = 2,
                Mail = "user@gmail.com",
                Password = "kfmmf87",
                FirstName = "Ana",
                LastName = "Binance",
                Age = 28,
            };
            var history1 = new MedicalHistory
            {
                Id = 1,
                Diseases = new List<string> { "Tuberculoza", "Cancer"},
                DrugsEverTaken = new List<Drug> { drug1},
            };

            var history2 = new MedicalHistory
            {
                Id = 2,
                Diseases = new List<string> { "Tuberculoza", "Raceala" },
                DrugsEverTaken = new List<Drug> { drug2 },
            };
            Schedule schedule1 = new Schedule
            {
                Id = 1,
                Date = DateOnly.FromDateTime(DateTime.Today),
                StartTime = TimeOnly.FromDateTime(DateTime.Now),
                EndTime = TimeOnly.FromDateTime(DateTime.Now)
            };

            Schedule schedule2 = new Schedule
            {
                Id = 2,
                Date = DateOnly.FromDateTime(DateTime.Today),
                StartTime = TimeOnly.FromDateTime(DateTime.Now),
                EndTime = TimeOnly.FromDateTime(DateTime.Now)
            };

            var doctor1 = new Doctor
            {
                Id = 1,
                Mail = "user@gmail.com",
                Password = "kfmmf87",
                Age = 69,
                FirstName = "Gheorghe",
                LastName = "Mama",
                WorkingDays = new List<Schedule> { schedule1, schedule2},
                Speciality = "Chirurg de Ochi",
            };

            var doctor2 = new Doctor
            {
                Id = 2,
                Mail = "idiot@gmail.com",
                Password = "smn6",
                Age = 20,
                FirstName = "Mioara",
                LastName = "Tata",
                WorkingDays = new List<Schedule> { schedule1, schedule2 },
                Speciality = "Chirurg de Mana",
            };
            patient1.MedicalHistory = history1;
            patient2.MedicalHistory = history2;

            var hospital1 = new Hospital
            {
                Id = 1,
                Name = "Toma Ciorba",
                Doctors = new List<Doctor> { doctor1, doctor2},
                Address = "Ion Cozma 35"
            };

            var hospital2 = new Hospital
            {
                Id = 2,
                Name = "Toma Burta",
                Doctors = new List<Doctor> { doctor2 },
                Address = "Ion Ciurca 32"
            };
            doctor1.Hospitals = new List<Hospital> { hospital1, hospital2 };
            doctor2.Hospitals = new List<Hospital> { hospital1};

            var appointment1 = new Appointment
            {
                Id = 1,
                Patient = patient1,
                Doctor = doctor2,
                Hospital = hospital1,
                Price = 120,
                StartTime = new DateTime(2019, 05, 09, 09, 15, 00),
                EndTime = new DateTime(2019, 05, 09, 09, 16, 00)
            };

            var appointment2 = new Appointment
            {
                Id = 2,
                Patient = patient2,
                Doctor = doctor1,
                Hospital = hospital2,
                Price = 150,
                StartTime = new DateTime(2019, 05, 09, 09, 15, 00),
                EndTime = new DateTime(2019, 05, 09, 09, 16, 00)
            };

            modelBuilder
                .Entity<Patient>()
                .HasData(new List<Patient> { patient1, patient2 });
            modelBuilder
                .Entity<Drug>()
                .HasData(new List<Drug> { drug1, drug2 });
            modelBuilder
                .Entity<MedicalHistory>()
                .HasData(new List<MedicalHistory> { history1, history2 });
            modelBuilder
                .Entity<Doctor>()
                .HasData(new List<Doctor> { doctor1, doctor2 });
            modelBuilder
                .Entity<Hospital>()
                .HasData(new List<Hospital> { hospital1, hospital2 });
            modelBuilder
                .Entity<Appointment>()
                .HasData(new List<Appointment> { appointment1, appointment2 });

        }

    }
}
