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
    [Migration("20221123171228_Third")]
    partial class Third
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "7.0.0");

            modelBuilder.Entity("MyDoctor.AppointmentInterval", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<Guid?>("AppointmentId")
                        .HasColumnType("TEXT");

                    b.Property<DateOnly>("Date")
                        .HasColumnType("TEXT");

                    b.Property<Guid>("DoctorId")
                        .HasColumnType("TEXT");

                    b.Property<TimeOnly>("EndTime")
                        .HasColumnType("TEXT");

                    b.Property<TimeOnly>("StartTime")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("DoctorId");

                    b.ToTable("AppointmentIntervals");
                });

            modelBuilder.Entity("MyDoctor.Domain.Models.Appointment", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<Guid>("AppointmentIntervalId")
                        .HasColumnType("TEXT");

                    b.Property<Guid>("AppointmentIntervalId1")
                        .HasColumnType("TEXT");

                    b.Property<Guid>("DoctorId")
                        .HasColumnType("TEXT");

                    b.Property<Guid>("PatientId")
                        .HasColumnType("TEXT");

                    b.Property<Guid?>("PrescriptionId")
                        .HasColumnType("TEXT");

                    b.Property<Guid>("PrescriptionId1")
                        .HasColumnType("TEXT");

                    b.Property<double>("Price")
                        .HasColumnType("REAL");

                    b.HasKey("Id");

                    b.HasIndex("AppointmentIntervalId1");

                    b.HasIndex("DoctorId");

                    b.HasIndex("PatientId");

                    b.HasIndex("PrescriptionId1");

                    b.ToTable("Appointments");
                });

            modelBuilder.Entity("MyDoctor.Domain.Models.Bill", b =>
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

                    b.ToTable("bills");
                });

            modelBuilder.Entity("MyDoctor.Domain.Models.Doctor", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Mail")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<Guid>("MedicalRoomId")
                        .HasColumnType("TEXT");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Speciality")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("MedicalRoomId");

                    b.ToTable("Doctors");
                });

            modelBuilder.Entity("MyDoctor.Domain.Models.Drug", b =>
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

                    b.Property<Guid?>("PrescriptionId")
                        .HasColumnType("TEXT");

                    b.Property<double>("Price")
                        .HasColumnType("REAL");

                    b.Property<uint>("Quantity")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("DrugStockId");

                    b.HasIndex("PrescriptionId");

                    b.ToTable("Drugs");
                });

            modelBuilder.Entity("MyDoctor.Domain.Models.DrugStock", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<Guid>("MedicalRoomId")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("DrugStocks");
                });

            modelBuilder.Entity("MyDoctor.Domain.Models.Hospital", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<string>("Adress")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Hospitals");
                });

            modelBuilder.Entity("MyDoctor.Domain.Models.HospitalAdmissionFile", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<Guid>("HospitalId")
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<Guid>("PrescriptionId")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("HospitalId");

                    b.ToTable("HospitalAdmissionFiles");
                });

            modelBuilder.Entity("MyDoctor.Domain.Models.MedicalHistory", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<Guid>("PatientId")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("MedicalHistories");
                });

            modelBuilder.Entity("MyDoctor.Domain.Models.MedicalRoom", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<string>("Adress")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<Guid>("DrugStockId")
                        .HasColumnType("TEXT");

                    b.Property<Guid>("DrugStockId1")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("DrugStockId1");

                    b.ToTable("MedicalRooms");
                });

            modelBuilder.Entity("MyDoctor.Domain.Models.Patient", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<uint>("Age")
                        .HasColumnType("INTEGER");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Mail")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<Guid>("MedicalHistoryId")
                        .HasColumnType("TEXT");

                    b.Property<Guid>("MedicalHistoryId1")
                        .HasColumnType("TEXT");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("MedicalHistoryId1");

                    b.ToTable("Patients");
                });

            modelBuilder.Entity("MyDoctor.Domain.Models.Prescription", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<Guid>("AppointmentId")
                        .HasColumnType("TEXT");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<Guid?>("HospitalAdmissionFileId")
                        .HasColumnType("TEXT");

                    b.Property<Guid>("HospitalAdmissionFileId1")
                        .HasColumnType("TEXT");

                    b.Property<Guid?>("MedicalHistoryId")
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("HospitalAdmissionFileId1");

                    b.HasIndex("MedicalHistoryId");

                    b.ToTable("Prescriptions");
                });

            modelBuilder.Entity("MyDoctor.Domain.Models.Procedure", b =>
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

            modelBuilder.Entity("MyDoctor.AppointmentInterval", b =>
                {
                    b.HasOne("MyDoctor.Domain.Models.Doctor", null)
                        .WithMany("AppointmentIntervals")
                        .HasForeignKey("DoctorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("MyDoctor.Domain.Models.Appointment", b =>
                {
                    b.HasOne("MyDoctor.AppointmentInterval", "AppointmentInterval")
                        .WithMany()
                        .HasForeignKey("AppointmentIntervalId1")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("MyDoctor.Domain.Models.Doctor", "Doctor")
                        .WithMany("Appointments")
                        .HasForeignKey("DoctorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("MyDoctor.Domain.Models.Patient", "Patient")
                        .WithMany("Appointments")
                        .HasForeignKey("PatientId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("MyDoctor.Domain.Models.Prescription", "Prescription")
                        .WithMany()
                        .HasForeignKey("PrescriptionId1")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("AppointmentInterval");

                    b.Navigation("Doctor");

                    b.Navigation("Patient");

                    b.Navigation("Prescription");
                });

            modelBuilder.Entity("MyDoctor.Domain.Models.Bill", b =>
                {
                    b.HasOne("MyDoctor.Domain.Models.Appointment", "Appointment")
                        .WithOne("Bill")
                        .HasForeignKey("MyDoctor.Domain.Models.Bill", "AppointmentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Appointment");
                });

            modelBuilder.Entity("MyDoctor.Domain.Models.Doctor", b =>
                {
                    b.HasOne("MyDoctor.Domain.Models.MedicalRoom", "MedicalRoom")
                        .WithMany("Doctors")
                        .HasForeignKey("MedicalRoomId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("MedicalRoom");
                });

            modelBuilder.Entity("MyDoctor.Domain.Models.Drug", b =>
                {
                    b.HasOne("MyDoctor.Domain.Models.DrugStock", null)
                        .WithMany("Drugs")
                        .HasForeignKey("DrugStockId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("MyDoctor.Domain.Models.Prescription", null)
                        .WithMany("Drugs")
                        .HasForeignKey("PrescriptionId");
                });

            modelBuilder.Entity("MyDoctor.Domain.Models.HospitalAdmissionFile", b =>
                {
                    b.HasOne("MyDoctor.Domain.Models.Hospital", "Hospital")
                        .WithMany()
                        .HasForeignKey("HospitalId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Hospital");
                });

            modelBuilder.Entity("MyDoctor.Domain.Models.MedicalRoom", b =>
                {
                    b.HasOne("MyDoctor.Domain.Models.DrugStock", "DrugStock")
                        .WithMany()
                        .HasForeignKey("DrugStockId1")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("DrugStock");
                });

            modelBuilder.Entity("MyDoctor.Domain.Models.Patient", b =>
                {
                    b.HasOne("MyDoctor.Domain.Models.MedicalHistory", "MedicalHistory")
                        .WithMany()
                        .HasForeignKey("MedicalHistoryId1")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("MedicalHistory");
                });

            modelBuilder.Entity("MyDoctor.Domain.Models.Prescription", b =>
                {
                    b.HasOne("MyDoctor.Domain.Models.HospitalAdmissionFile", "HospitalAdmissionFile")
                        .WithMany()
                        .HasForeignKey("HospitalAdmissionFileId1")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("MyDoctor.Domain.Models.MedicalHistory", null)
                        .WithMany("Prescriptions")
                        .HasForeignKey("MedicalHistoryId");

                    b.Navigation("HospitalAdmissionFile");
                });

            modelBuilder.Entity("MyDoctor.Domain.Models.Procedure", b =>
                {
                    b.HasOne("MyDoctor.Domain.Models.Prescription", "Prescription")
                        .WithMany("Procedures")
                        .HasForeignKey("PrescriptionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Prescription");
                });

            modelBuilder.Entity("MyDoctor.Domain.Models.Appointment", b =>
                {
                    b.Navigation("Bill")
                        .IsRequired();
                });

            modelBuilder.Entity("MyDoctor.Domain.Models.Doctor", b =>
                {
                    b.Navigation("AppointmentIntervals");

                    b.Navigation("Appointments");
                });

            modelBuilder.Entity("MyDoctor.Domain.Models.DrugStock", b =>
                {
                    b.Navigation("Drugs");
                });

            modelBuilder.Entity("MyDoctor.Domain.Models.MedicalHistory", b =>
                {
                    b.Navigation("Prescriptions");
                });

            modelBuilder.Entity("MyDoctor.Domain.Models.MedicalRoom", b =>
                {
                    b.Navigation("Doctors");
                });

            modelBuilder.Entity("MyDoctor.Domain.Models.Patient", b =>
                {
                    b.Navigation("Appointments");
                });

            modelBuilder.Entity("MyDoctor.Domain.Models.Prescription", b =>
                {
                    b.Navigation("Drugs");

                    b.Navigation("Procedures");
                });
#pragma warning restore 612, 618
        }
    }
}
