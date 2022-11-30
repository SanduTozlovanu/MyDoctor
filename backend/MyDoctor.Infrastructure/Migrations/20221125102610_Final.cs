using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MyDoctorApp.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Final : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ScheduleInterval_Doctors_DoctorId",
                table: "ScheduleInterval");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ScheduleInterval",
                table: "ScheduleInterval");

            migrationBuilder.RenameTable(
                name: "ScheduleInterval",
                newName: "ScheduleIntervals");

            migrationBuilder.RenameIndex(
                name: "IX_ScheduleInterval_DoctorId",
                table: "ScheduleIntervals",
                newName: "IX_ScheduleIntervals_DoctorId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ScheduleIntervals",
                table: "ScheduleIntervals",
                column: "Id");

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
                name: "FK_ScheduleIntervals_Doctors_DoctorId",
                table: "ScheduleIntervals");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ScheduleIntervals",
                table: "ScheduleIntervals");

            migrationBuilder.RenameTable(
                name: "ScheduleIntervals",
                newName: "ScheduleInterval");

            migrationBuilder.RenameIndex(
                name: "IX_ScheduleIntervals_DoctorId",
                table: "ScheduleInterval",
                newName: "IX_ScheduleInterval_DoctorId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ScheduleInterval",
                table: "ScheduleInterval",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ScheduleInterval_Doctors_DoctorId",
                table: "ScheduleInterval",
                column: "DoctorId",
                principalTable: "Doctors",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
