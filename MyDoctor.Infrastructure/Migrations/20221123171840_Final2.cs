using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MyDoctorApp.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Final2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Prescriptions_HospitalAdmissionFiles_HospitalAdmissionFileId1",
                table: "Prescriptions");

            migrationBuilder.DropIndex(
                name: "IX_Prescriptions_HospitalAdmissionFileId1",
                table: "Prescriptions");

            migrationBuilder.DropColumn(
                name: "HospitalAdmissionFileId",
                table: "Prescriptions");

            migrationBuilder.DropColumn(
                name: "HospitalAdmissionFileId1",
                table: "Prescriptions");

            migrationBuilder.CreateIndex(
                name: "IX_HospitalAdmissionFiles_PrescriptionId",
                table: "HospitalAdmissionFiles",
                column: "PrescriptionId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_HospitalAdmissionFiles_Prescriptions_PrescriptionId",
                table: "HospitalAdmissionFiles",
                column: "PrescriptionId",
                principalTable: "Prescriptions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_HospitalAdmissionFiles_Prescriptions_PrescriptionId",
                table: "HospitalAdmissionFiles");

            migrationBuilder.DropIndex(
                name: "IX_HospitalAdmissionFiles_PrescriptionId",
                table: "HospitalAdmissionFiles");

            migrationBuilder.AddColumn<Guid>(
                name: "HospitalAdmissionFileId",
                table: "Prescriptions",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "HospitalAdmissionFileId1",
                table: "Prescriptions",
                type: "TEXT",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_Prescriptions_HospitalAdmissionFileId1",
                table: "Prescriptions",
                column: "HospitalAdmissionFileId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Prescriptions_HospitalAdmissionFiles_HospitalAdmissionFileId1",
                table: "Prescriptions",
                column: "HospitalAdmissionFileId1",
                principalTable: "HospitalAdmissionFiles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
