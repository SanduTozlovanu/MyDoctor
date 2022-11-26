using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MyDoctorApp.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Initi4 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Interval_Appointments_AppointmentId",
                table: "Interval");

            migrationBuilder.DropForeignKey(
                name: "FK_Interval_Doctors_DoctorId",
                table: "Interval");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Interval",
                table: "Interval");

            migrationBuilder.DropIndex(
                name: "IX_Interval_DoctorId",
                table: "Interval");

            migrationBuilder.DropColumn(
                name: "Discriminator",
                table: "Interval");

            migrationBuilder.DropColumn(
                name: "DoctorId",
                table: "Interval");

            migrationBuilder.RenameTable(
                name: "Interval",
                newName: "AppointmentIntervals");

            migrationBuilder.RenameIndex(
                name: "IX_Interval_AppointmentId",
                table: "AppointmentIntervals",
                newName: "IX_AppointmentIntervals_AppointmentId");

            migrationBuilder.AlterColumn<Guid>(
                name: "AppointmentId",
                table: "AppointmentIntervals",
                type: "TEXT",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "TEXT",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_AppointmentIntervals",
                table: "AppointmentIntervals",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "ScheduleInterval",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    DoctorId = table.Column<Guid>(type: "TEXT", nullable: false),
                    Date = table.Column<DateOnly>(type: "TEXT", nullable: false),
                    StartTime = table.Column<TimeOnly>(type: "TEXT", nullable: false),
                    EndTime = table.Column<TimeOnly>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ScheduleInterval", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ScheduleInterval_Doctors_DoctorId",
                        column: x => x.DoctorId,
                        principalTable: "Doctors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ScheduleInterval_DoctorId",
                table: "ScheduleInterval",
                column: "DoctorId");

            migrationBuilder.AddForeignKey(
                name: "FK_AppointmentIntervals_Appointments_AppointmentId",
                table: "AppointmentIntervals",
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

            migrationBuilder.DropTable(
                name: "ScheduleInterval");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AppointmentIntervals",
                table: "AppointmentIntervals");

            migrationBuilder.RenameTable(
                name: "AppointmentIntervals",
                newName: "Interval");

            migrationBuilder.RenameIndex(
                name: "IX_AppointmentIntervals_AppointmentId",
                table: "Interval",
                newName: "IX_Interval_AppointmentId");

            migrationBuilder.AlterColumn<Guid>(
                name: "AppointmentId",
                table: "Interval",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "TEXT");

            migrationBuilder.AddColumn<string>(
                name: "Discriminator",
                table: "Interval",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<Guid>(
                name: "DoctorId",
                table: "Interval",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Interval",
                table: "Interval",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Interval_DoctorId",
                table: "Interval",
                column: "DoctorId");

            migrationBuilder.AddForeignKey(
                name: "FK_Interval_Appointments_AppointmentId",
                table: "Interval",
                column: "AppointmentId",
                principalTable: "Appointments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Interval_Doctors_DoctorId",
                table: "Interval",
                column: "DoctorId",
                principalTable: "Doctors",
                principalColumn: "Id");
        }
    }
}
