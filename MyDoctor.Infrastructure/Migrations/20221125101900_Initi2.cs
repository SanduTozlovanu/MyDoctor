using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MyDoctorApp.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Initi2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Appointments_Interval_AppointmentIntervalId",
                table: "Appointments");

            migrationBuilder.DropIndex(
                name: "IX_Interval_AppointmentId",
                table: "Interval");

            migrationBuilder.DropIndex(
                name: "IX_Appointments_AppointmentIntervalId",
                table: "Appointments");

            migrationBuilder.DropColumn(
                name: "AppointmentIntervalId",
                table: "Appointments");

            migrationBuilder.CreateIndex(
                name: "IX_Interval_AppointmentId",
                table: "Interval",
                column: "AppointmentId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Interval_AppointmentId",
                table: "Interval");

            migrationBuilder.AddColumn<Guid>(
                name: "AppointmentIntervalId",
                table: "Appointments",
                type: "TEXT",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_Interval_AppointmentId",
                table: "Interval",
                column: "AppointmentId");

            migrationBuilder.CreateIndex(
                name: "IX_Appointments_AppointmentIntervalId",
                table: "Appointments",
                column: "AppointmentIntervalId");

            migrationBuilder.AddForeignKey(
                name: "FK_Appointments_Interval_AppointmentIntervalId",
                table: "Appointments",
                column: "AppointmentIntervalId",
                principalTable: "Interval",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
