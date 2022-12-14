﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using MyDoctorApp.Infrastructure;

#nullable disable

namespace MyDoctorApp.Infrastructure.Migrations
{
    [DbContext(typeof(DatabaseContext))]
    [Migration("20221214141435_Specialities1")]
    partial class Specialities1
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "7.0.0");

            modelBuilder.Entity("MyDoctorApp.Domain.Models.Appointment", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<Guid>("DoctorId")
                        .HasColumnType("TEXT");

                    b.Property<Guid>("PatientId")
                        .HasColumnType("TEXT");

                    b.Property<double>("Price")
                        .HasColumnType("REAL");

                    b.HasKey("Id");

                    b.HasIndex("DoctorId");

                    b.HasIndex("PatientId");

                    b.ToTable("Appointments");
                });

            modelBuilder.Entity("MyDoctorApp.Domain.Models.AppointmentInterval", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<Guid>("AppointmentId")
                        .HasColumnType("TEXT");

                    b.Property<DateOnly>("Date")
                        .HasColumnType("TEXT");

                    b.Property<TimeOnly>("EndTime")
                        .HasColumnType("TEXT");

                    b.Property<TimeOnly>("StartTime")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("AppointmentId")
                        .IsUnique();

                    b.ToTable("AppointmentIntervals");
                });

            modelBuilder.Entity("MyDoctorApp.Domain.Models.Bill", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<Guid>("AppointmentId")
                        .HasColumnType("TEXT");

                    b.Property<double>("BillPrice")
                        .HasColumnType("REAL");

                    b.HasKey("Id");

                    b.HasIndex("AppointmentId")
                        .IsUnique();

                    b.ToTable("Bills");
                });

            modelBuilder.Entity("MyDoctorApp.Domain.Models.Doctor", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<string>("AccountType")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<Guid>("MedicalRoomId")
                        .HasColumnType("TEXT");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<Guid>("SpecialityID")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("MedicalRoomId");

                    b.HasIndex("SpecialityID");

                    b.ToTable("Doctors");
                });

            modelBuilder.Entity("MyDoctorApp.Domain.Models.Drug", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<Guid>("DrugStockId")
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<double>("Price")
                        .HasColumnType("REAL");

                    b.Property<uint>("Quantity")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("DrugStockId");

                    b.ToTable("Drugs");
                });

            modelBuilder.Entity("MyDoctorApp.Domain.Models.DrugStock", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<Guid>("MedicalRoomId")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("MedicalRoomId")
                        .IsUnique();

                    b.ToTable("DrugStocks");
                });

            modelBuilder.Entity("MyDoctorApp.Domain.Models.MedicalHistory", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<Guid>("PatientId")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("PatientId")
                        .IsUnique();

                    b.ToTable("MedicalHistories");
                });

            modelBuilder.Entity("MyDoctorApp.Domain.Models.MedicalRoom", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<string>("Adress")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("MedicalRooms");
                });

            modelBuilder.Entity("MyDoctorApp.Domain.Models.Patient", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<string>("AccountType")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<uint>("Age")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Patients");
                });

            modelBuilder.Entity("MyDoctorApp.Domain.Models.PrescriptedDrug", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<Guid>("DrugId")
                        .HasColumnType("TEXT");

                    b.Property<Guid>("PrescriptionId")
                        .HasColumnType("TEXT");

                    b.Property<uint>("Quantity")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("DrugId");

                    b.HasIndex("PrescriptionId");

                    b.ToTable("PrescriptedDrugs");
                });

            modelBuilder.Entity("MyDoctorApp.Domain.Models.Prescription", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<Guid>("AppointmentId")
                        .HasColumnType("TEXT");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<Guid?>("MedicalHistoryId")
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("AppointmentId")
                        .IsUnique();

                    b.HasIndex("MedicalHistoryId");

                    b.ToTable("Prescriptions");
                });

            modelBuilder.Entity("MyDoctorApp.Domain.Models.Procedure", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<Guid>("PrescriptionId")
                        .HasColumnType("TEXT");

                    b.Property<double>("Price")
                        .HasColumnType("REAL");

                    b.HasKey("Id");

                    b.HasIndex("PrescriptionId");

                    b.ToTable("Procedures");
                });

            modelBuilder.Entity("MyDoctorApp.Domain.Models.ScheduleInterval", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<string>("DayOfWeek")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<Guid>("DoctorId")
                        .HasColumnType("TEXT");

                    b.Property<TimeOnly>("EndTime")
                        .HasColumnType("TEXT");

                    b.Property<TimeOnly>("StartTime")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("DoctorId");

                    b.ToTable("ScheduleIntervals");
                });

            modelBuilder.Entity("MyDoctorApp.Domain.Models.Speciality", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Specialities");
                });

            modelBuilder.Entity("MyDoctorApp.Domain.Models.Appointment", b =>
                {
                    b.HasOne("MyDoctorApp.Domain.Models.Doctor", "Doctor")
                        .WithMany("Appointments")
                        .HasForeignKey("DoctorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("MyDoctorApp.Domain.Models.Patient", "Patient")
                        .WithMany("Appointments")
                        .HasForeignKey("PatientId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Doctor");

                    b.Navigation("Patient");
                });

            modelBuilder.Entity("MyDoctorApp.Domain.Models.AppointmentInterval", b =>
                {
                    b.HasOne("MyDoctorApp.Domain.Models.Appointment", "Appointment")
                        .WithOne("AppointmentInterval")
                        .HasForeignKey("MyDoctorApp.Domain.Models.AppointmentInterval", "AppointmentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Appointment");
                });

            modelBuilder.Entity("MyDoctorApp.Domain.Models.Bill", b =>
                {
                    b.HasOne("MyDoctorApp.Domain.Models.Appointment", "Appointment")
                        .WithOne("Bill")
                        .HasForeignKey("MyDoctorApp.Domain.Models.Bill", "AppointmentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Appointment");
                });

            modelBuilder.Entity("MyDoctorApp.Domain.Models.Doctor", b =>
                {
                    b.HasOne("MyDoctorApp.Domain.Models.MedicalRoom", "MedicalRoom")
                        .WithMany("Doctors")
                        .HasForeignKey("MedicalRoomId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("MyDoctorApp.Domain.Models.Speciality", "Speciality")
                        .WithMany("Doctors")
                        .HasForeignKey("SpecialityID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("MedicalRoom");

                    b.Navigation("Speciality");
                });

            modelBuilder.Entity("MyDoctorApp.Domain.Models.Drug", b =>
                {
                    b.HasOne("MyDoctorApp.Domain.Models.DrugStock", "DrugStock")
                        .WithMany("Drugs")
                        .HasForeignKey("DrugStockId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("DrugStock");
                });

            modelBuilder.Entity("MyDoctorApp.Domain.Models.DrugStock", b =>
                {
                    b.HasOne("MyDoctorApp.Domain.Models.MedicalRoom", "MedicalRoom")
                        .WithOne("DrugStock")
                        .HasForeignKey("MyDoctorApp.Domain.Models.DrugStock", "MedicalRoomId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("MedicalRoom");
                });

            modelBuilder.Entity("MyDoctorApp.Domain.Models.MedicalHistory", b =>
                {
                    b.HasOne("MyDoctorApp.Domain.Models.Patient", "Patient")
                        .WithOne("MedicalHistory")
                        .HasForeignKey("MyDoctorApp.Domain.Models.MedicalHistory", "PatientId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Patient");
                });

            modelBuilder.Entity("MyDoctorApp.Domain.Models.PrescriptedDrug", b =>
                {
                    b.HasOne("MyDoctorApp.Domain.Models.Drug", "Drug")
                        .WithMany()
                        .HasForeignKey("DrugId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("MyDoctorApp.Domain.Models.Prescription", "Prescription")
                        .WithMany("PrescriptedDrugs")
                        .HasForeignKey("PrescriptionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Drug");

                    b.Navigation("Prescription");
                });

            modelBuilder.Entity("MyDoctorApp.Domain.Models.Prescription", b =>
                {
                    b.HasOne("MyDoctorApp.Domain.Models.Appointment", "Appointment")
                        .WithOne("Prescription")
                        .HasForeignKey("MyDoctorApp.Domain.Models.Prescription", "AppointmentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("MyDoctorApp.Domain.Models.MedicalHistory", null)
                        .WithMany("Prescriptions")
                        .HasForeignKey("MedicalHistoryId");

                    b.Navigation("Appointment");
                });

            modelBuilder.Entity("MyDoctorApp.Domain.Models.Procedure", b =>
                {
                    b.HasOne("MyDoctorApp.Domain.Models.Prescription", "Prescription")
                        .WithMany("Procedures")
                        .HasForeignKey("PrescriptionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Prescription");
                });

            modelBuilder.Entity("MyDoctorApp.Domain.Models.ScheduleInterval", b =>
                {
                    b.HasOne("MyDoctorApp.Domain.Models.Doctor", "Doctor")
                        .WithMany("ScheduleIntervals")
                        .HasForeignKey("DoctorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Doctor");
                });

            modelBuilder.Entity("MyDoctorApp.Domain.Models.Appointment", b =>
                {
                    b.Navigation("AppointmentInterval")
                        .IsRequired();

                    b.Navigation("Bill");

                    b.Navigation("Prescription");
                });

            modelBuilder.Entity("MyDoctorApp.Domain.Models.Doctor", b =>
                {
                    b.Navigation("Appointments");

                    b.Navigation("ScheduleIntervals");
                });

            modelBuilder.Entity("MyDoctorApp.Domain.Models.DrugStock", b =>
                {
                    b.Navigation("Drugs");
                });

            modelBuilder.Entity("MyDoctorApp.Domain.Models.MedicalHistory", b =>
                {
                    b.Navigation("Prescriptions");
                });

            modelBuilder.Entity("MyDoctorApp.Domain.Models.MedicalRoom", b =>
                {
                    b.Navigation("Doctors");

                    b.Navigation("DrugStock")
                        .IsRequired();
                });

            modelBuilder.Entity("MyDoctorApp.Domain.Models.Patient", b =>
                {
                    b.Navigation("Appointments");

                    b.Navigation("MedicalHistory")
                        .IsRequired();
                });

            modelBuilder.Entity("MyDoctorApp.Domain.Models.Prescription", b =>
                {
                    b.Navigation("PrescriptedDrugs");

                    b.Navigation("Procedures");
                });

            modelBuilder.Entity("MyDoctorApp.Domain.Models.Speciality", b =>
                {
                    b.Navigation("Doctors");
                });
#pragma warning restore 612, 618
        }
    }
}