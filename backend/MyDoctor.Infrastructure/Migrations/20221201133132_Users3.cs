using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MyDoctorApp.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Users3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Appointments_User_DoctorId",
                table: "Appointments");

            migrationBuilder.DropForeignKey(
                name: "FK_Appointments_User_PatientId",
                table: "Appointments");

            migrationBuilder.DropForeignKey(
                name: "FK_MedicalHistories_User_PatientId",
                table: "MedicalHistories");

            migrationBuilder.DropForeignKey(
                name: "FK_ScheduleIntervals_User_DoctorId",
                table: "ScheduleIntervals");

            migrationBuilder.DropForeignKey(
                name: "FK_User_MedicalRooms_MedicalRoomId",
                table: "User");

            migrationBuilder.DropPrimaryKey(
                name: "PK_User",
                table: "User");

            migrationBuilder.DropIndex(
                name: "IX_User_Mail",
                table: "User");

            migrationBuilder.DropIndex(
                name: "IX_User_MedicalRoomId",
                table: "User");

            migrationBuilder.DropColumn(
                name: "Discriminator",
                table: "User");

            migrationBuilder.DropColumn(
                name: "MedicalRoomId",
                table: "User");

            migrationBuilder.DropColumn(
                name: "Speciality",
                table: "User");

            migrationBuilder.RenameTable(
                name: "User",
                newName: "Patients");

            migrationBuilder.AlterColumn<uint>(
                name: "Age",
                table: "Patients",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0u,
                oldClrType: typeof(uint),
                oldType: "INTEGER",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Patients",
                table: "Patients",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "Doctors",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    MedicalRoomId = table.Column<Guid>(type: "TEXT", nullable: false),
                    Speciality = table.Column<string>(type: "TEXT", nullable: false),
                    AccountType = table.Column<string>(type: "TEXT", nullable: false),
                    Mail = table.Column<string>(type: "TEXT", nullable: false),
                    Password = table.Column<string>(type: "TEXT", nullable: false),
                    FirstName = table.Column<string>(type: "TEXT", nullable: false),
                    LastName = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Doctors", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Doctors_MedicalRooms_MedicalRoomId",
                        column: x => x.MedicalRoomId,
                        principalTable: "MedicalRooms",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Doctors_MedicalRoomId",
                table: "Doctors",
                column: "MedicalRoomId");

            migrationBuilder.AddForeignKey(
                name: "FK_Appointments_Doctors_DoctorId",
                table: "Appointments",
                column: "DoctorId",
                principalTable: "Doctors",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Appointments_Patients_PatientId",
                table: "Appointments",
                column: "PatientId",
                principalTable: "Patients",
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
                name: "FK_ScheduleIntervals_Doctors_DoctorId",
                table: "ScheduleIntervals",
                column: "DoctorId",
                principalTable: "Doctors",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Appointments_Doctors_DoctorId",
                table: "Appointments");

            migrationBuilder.DropForeignKey(
                name: "FK_Appointments_Patients_PatientId",
                table: "Appointments");

            migrationBuilder.DropForeignKey(
                name: "FK_MedicalHistories_Patients_PatientId",
                table: "MedicalHistories");

            migrationBuilder.DropForeignKey(
                name: "FK_ScheduleIntervals_Doctors_DoctorId",
                table: "ScheduleIntervals");

            migrationBuilder.DropTable(
                name: "Doctors");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Patients",
                table: "Patients");

            migrationBuilder.RenameTable(
                name: "Patients",
                newName: "User");

            migrationBuilder.AlterColumn<uint>(
                name: "Age",
                table: "User",
                type: "INTEGER",
                nullable: true,
                oldClrType: typeof(uint),
                oldType: "INTEGER");

            migrationBuilder.AddColumn<string>(
                name: "Discriminator",
                table: "User",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<Guid>(
                name: "MedicalRoomId",
                table: "User",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Speciality",
                table: "User",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_User",
                table: "User",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_User_Mail",
                table: "User",
                column: "Mail",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_User_MedicalRoomId",
                table: "User",
                column: "MedicalRoomId");

            migrationBuilder.AddForeignKey(
                name: "FK_Appointments_User_DoctorId",
                table: "Appointments",
                column: "DoctorId",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Appointments_User_PatientId",
                table: "Appointments",
                column: "PatientId",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MedicalHistories_User_PatientId",
                table: "MedicalHistories",
                column: "PatientId",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ScheduleIntervals_User_DoctorId",
                table: "ScheduleIntervals",
                column: "DoctorId",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_User_MedicalRooms_MedicalRoomId",
                table: "User",
                column: "MedicalRoomId",
                principalTable: "MedicalRooms",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
