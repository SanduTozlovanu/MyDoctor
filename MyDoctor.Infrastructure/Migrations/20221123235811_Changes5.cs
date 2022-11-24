using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MyDoctorApp.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Changes5 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AppointmentIntervals_Appointments_AppointmentId",
                table: "AppointmentIntervals");

            migrationBuilder.DropForeignKey(
                name: "FK_AppointmentIntervals_Doctors_DoctorId",
                table: "AppointmentIntervals");

            migrationBuilder.AlterColumn<Guid>(
                name: "DoctorId",
                table: "AppointmentIntervals",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "TEXT");

            migrationBuilder.AlterColumn<Guid>(
                name: "AppointmentId",
                table: "AppointmentIntervals",
                type: "TEXT",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "TEXT",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_AppointmentIntervals_Appointments_AppointmentId",
                table: "AppointmentIntervals",
                column: "AppointmentId",
                principalTable: "Appointments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AppointmentIntervals_Doctors_DoctorId",
                table: "AppointmentIntervals",
                column: "DoctorId",
                principalTable: "Doctors",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AppointmentIntervals_Appointments_AppointmentId",
                table: "AppointmentIntervals");

            migrationBuilder.DropForeignKey(
                name: "FK_AppointmentIntervals_Doctors_DoctorId",
                table: "AppointmentIntervals");

            migrationBuilder.AlterColumn<Guid>(
                name: "DoctorId",
                table: "AppointmentIntervals",
                type: "TEXT",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "TEXT",
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "AppointmentId",
                table: "AppointmentIntervals",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "TEXT");

            migrationBuilder.AddForeignKey(
                name: "FK_AppointmentIntervals_Appointments_AppointmentId",
                table: "AppointmentIntervals",
                column: "AppointmentId",
                principalTable: "Appointments",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AppointmentIntervals_Doctors_DoctorId",
                table: "AppointmentIntervals",
                column: "DoctorId",
                principalTable: "Doctors",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
