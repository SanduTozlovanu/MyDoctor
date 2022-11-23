using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MyDoctorApp.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class SecondMig : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ApointmentId",
                table: "bills",
                newName: "AppointmentId");

            migrationBuilder.AddColumn<Guid>(
                name: "HospitalAdmissionFileId1",
                table: "Prescriptions",
                type: "TEXT",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "MedicalHistoryId1",
                table: "Patients",
                type: "TEXT",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "DrugStockId1",
                table: "MedicalRooms",
                type: "TEXT",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<double>(
                name: "BillPrice",
                table: "bills",
                type: "REAL",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<Guid>(
                name: "AppointmentIntervalId1",
                table: "Appointments",
                type: "TEXT",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "BillId1",
                table: "Appointments",
                type: "TEXT",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "PrescriptionId1",
                table: "Appointments",
                type: "TEXT",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_Prescriptions_HospitalAdmissionFileId1",
                table: "Prescriptions",
                column: "HospitalAdmissionFileId1");

            migrationBuilder.CreateIndex(
                name: "IX_Patients_MedicalHistoryId1",
                table: "Patients",
                column: "MedicalHistoryId1");

            migrationBuilder.CreateIndex(
                name: "IX_MedicalRooms_DrugStockId1",
                table: "MedicalRooms",
                column: "DrugStockId1");

            migrationBuilder.CreateIndex(
                name: "IX_HospitalAdmissionFiles_HospitalId",
                table: "HospitalAdmissionFiles",
                column: "HospitalId");

            migrationBuilder.CreateIndex(
                name: "IX_Appointments_AppointmentIntervalId1",
                table: "Appointments",
                column: "AppointmentIntervalId1");

            migrationBuilder.CreateIndex(
                name: "IX_Appointments_BillId1",
                table: "Appointments",
                column: "BillId1");

            migrationBuilder.CreateIndex(
                name: "IX_Appointments_PrescriptionId1",
                table: "Appointments",
                column: "PrescriptionId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Appointments_AppointmentIntervals_AppointmentIntervalId1",
                table: "Appointments",
                column: "AppointmentIntervalId1",
                principalTable: "AppointmentIntervals",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Appointments_Prescriptions_PrescriptionId1",
                table: "Appointments",
                column: "PrescriptionId1",
                principalTable: "Prescriptions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Appointments_bills_BillId1",
                table: "Appointments",
                column: "BillId1",
                principalTable: "bills",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_HospitalAdmissionFiles_Hospitals_HospitalId",
                table: "HospitalAdmissionFiles",
                column: "HospitalId",
                principalTable: "Hospitals",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MedicalRooms_DrugStocks_DrugStockId1",
                table: "MedicalRooms",
                column: "DrugStockId1",
                principalTable: "DrugStocks",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Patients_MedicalHistories_MedicalHistoryId1",
                table: "Patients",
                column: "MedicalHistoryId1",
                principalTable: "MedicalHistories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Prescriptions_HospitalAdmissionFiles_HospitalAdmissionFileId1",
                table: "Prescriptions",
                column: "HospitalAdmissionFileId1",
                principalTable: "HospitalAdmissionFiles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Appointments_AppointmentIntervals_AppointmentIntervalId1",
                table: "Appointments");

            migrationBuilder.DropForeignKey(
                name: "FK_Appointments_Prescriptions_PrescriptionId1",
                table: "Appointments");

            migrationBuilder.DropForeignKey(
                name: "FK_Appointments_bills_BillId1",
                table: "Appointments");

            migrationBuilder.DropForeignKey(
                name: "FK_HospitalAdmissionFiles_Hospitals_HospitalId",
                table: "HospitalAdmissionFiles");

            migrationBuilder.DropForeignKey(
                name: "FK_MedicalRooms_DrugStocks_DrugStockId1",
                table: "MedicalRooms");

            migrationBuilder.DropForeignKey(
                name: "FK_Patients_MedicalHistories_MedicalHistoryId1",
                table: "Patients");

            migrationBuilder.DropForeignKey(
                name: "FK_Prescriptions_HospitalAdmissionFiles_HospitalAdmissionFileId1",
                table: "Prescriptions");

            migrationBuilder.DropIndex(
                name: "IX_Prescriptions_HospitalAdmissionFileId1",
                table: "Prescriptions");

            migrationBuilder.DropIndex(
                name: "IX_Patients_MedicalHistoryId1",
                table: "Patients");

            migrationBuilder.DropIndex(
                name: "IX_MedicalRooms_DrugStockId1",
                table: "MedicalRooms");

            migrationBuilder.DropIndex(
                name: "IX_HospitalAdmissionFiles_HospitalId",
                table: "HospitalAdmissionFiles");

            migrationBuilder.DropIndex(
                name: "IX_Appointments_AppointmentIntervalId1",
                table: "Appointments");

            migrationBuilder.DropIndex(
                name: "IX_Appointments_BillId1",
                table: "Appointments");

            migrationBuilder.DropIndex(
                name: "IX_Appointments_PrescriptionId1",
                table: "Appointments");

            migrationBuilder.DropColumn(
                name: "HospitalAdmissionFileId1",
                table: "Prescriptions");

            migrationBuilder.DropColumn(
                name: "MedicalHistoryId1",
                table: "Patients");

            migrationBuilder.DropColumn(
                name: "DrugStockId1",
                table: "MedicalRooms");

            migrationBuilder.DropColumn(
                name: "BillPrice",
                table: "bills");

            migrationBuilder.DropColumn(
                name: "AppointmentIntervalId1",
                table: "Appointments");

            migrationBuilder.DropColumn(
                name: "BillId1",
                table: "Appointments");

            migrationBuilder.DropColumn(
                name: "PrescriptionId1",
                table: "Appointments");

            migrationBuilder.RenameColumn(
                name: "AppointmentId",
                table: "bills",
                newName: "ApointmentId");
        }
    }
}
