using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MyDoctorApp.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Final1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Appointments_AppointmentIntervals_AppointmentIntervalId1",
                table: "Appointments");

            migrationBuilder.DropForeignKey(
                name: "FK_Appointments_Prescriptions_PrescriptionId1",
                table: "Appointments");

            migrationBuilder.DropForeignKey(
                name: "FK_MedicalRooms_DrugStocks_DrugStockId1",
                table: "MedicalRooms");

            migrationBuilder.DropForeignKey(
                name: "FK_Patients_MedicalHistories_MedicalHistoryId1",
                table: "Patients");

            migrationBuilder.DropIndex(
                name: "IX_Patients_MedicalHistoryId1",
                table: "Patients");

            migrationBuilder.DropIndex(
                name: "IX_MedicalRooms_DrugStockId1",
                table: "MedicalRooms");

            migrationBuilder.DropIndex(
                name: "IX_Appointments_AppointmentIntervalId1",
                table: "Appointments");

            migrationBuilder.DropIndex(
                name: "IX_Appointments_PrescriptionId1",
                table: "Appointments");

            migrationBuilder.DropColumn(
                name: "MedicalHistoryId",
                table: "Patients");

            migrationBuilder.DropColumn(
                name: "MedicalHistoryId1",
                table: "Patients");

            migrationBuilder.DropColumn(
                name: "DrugStockId",
                table: "MedicalRooms");

            migrationBuilder.DropColumn(
                name: "DrugStockId1",
                table: "MedicalRooms");

            migrationBuilder.DropColumn(
                name: "AppointmentIntervalId",
                table: "Appointments");

            migrationBuilder.DropColumn(
                name: "AppointmentIntervalId1",
                table: "Appointments");

            migrationBuilder.DropColumn(
                name: "PrescriptionId",
                table: "Appointments");

            migrationBuilder.DropColumn(
                name: "PrescriptionId1",
                table: "Appointments");

            migrationBuilder.CreateIndex(
                name: "IX_Prescriptions_AppointmentId",
                table: "Prescriptions",
                column: "AppointmentId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_MedicalHistories_PatientId",
                table: "MedicalHistories",
                column: "PatientId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_DrugStocks_MedicalRoomId",
                table: "DrugStocks",
                column: "MedicalRoomId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AppointmentIntervals_AppointmentId",
                table: "AppointmentIntervals",
                column: "AppointmentId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_AppointmentIntervals_Appointments_AppointmentId",
                table: "AppointmentIntervals",
                column: "AppointmentId",
                principalTable: "Appointments",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_DrugStocks_MedicalRooms_MedicalRoomId",
                table: "DrugStocks",
                column: "MedicalRoomId",
                principalTable: "MedicalRooms",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MedicalHistories_Patients_PatientId",
                table: "MedicalHistories",
                column: "PatientId",
                principalTable: "Patients",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Prescriptions_Appointments_AppointmentId",
                table: "Prescriptions",
                column: "AppointmentId",
                principalTable: "Appointments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AppointmentIntervals_Appointments_AppointmentId",
                table: "AppointmentIntervals");

            migrationBuilder.DropForeignKey(
                name: "FK_DrugStocks_MedicalRooms_MedicalRoomId",
                table: "DrugStocks");

            migrationBuilder.DropForeignKey(
                name: "FK_MedicalHistories_Patients_PatientId",
                table: "MedicalHistories");

            migrationBuilder.DropForeignKey(
                name: "FK_Prescriptions_Appointments_AppointmentId",
                table: "Prescriptions");

            migrationBuilder.DropIndex(
                name: "IX_Prescriptions_AppointmentId",
                table: "Prescriptions");

            migrationBuilder.DropIndex(
                name: "IX_MedicalHistories_PatientId",
                table: "MedicalHistories");

            migrationBuilder.DropIndex(
                name: "IX_DrugStocks_MedicalRoomId",
                table: "DrugStocks");

            migrationBuilder.DropIndex(
                name: "IX_AppointmentIntervals_AppointmentId",
                table: "AppointmentIntervals");

            migrationBuilder.AddColumn<Guid>(
                name: "MedicalHistoryId",
                table: "Patients",
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
                name: "DrugStockId",
                table: "MedicalRooms",
                type: "TEXT",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "DrugStockId1",
                table: "MedicalRooms",
                type: "TEXT",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "AppointmentIntervalId",
                table: "Appointments",
                type: "TEXT",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "AppointmentIntervalId1",
                table: "Appointments",
                type: "TEXT",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "PrescriptionId",
                table: "Appointments",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "PrescriptionId1",
                table: "Appointments",
                type: "TEXT",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_Patients_MedicalHistoryId1",
                table: "Patients",
                column: "MedicalHistoryId1");

            migrationBuilder.CreateIndex(
                name: "IX_MedicalRooms_DrugStockId1",
                table: "MedicalRooms",
                column: "DrugStockId1");

            migrationBuilder.CreateIndex(
                name: "IX_Appointments_AppointmentIntervalId1",
                table: "Appointments",
                column: "AppointmentIntervalId1");

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
        }
    }
}
