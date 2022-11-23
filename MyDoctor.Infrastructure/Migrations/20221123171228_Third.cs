using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MyDoctorApp.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Third : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Appointments_bills_BillId1",
                table: "Appointments");

            migrationBuilder.DropIndex(
                name: "IX_Appointments_BillId1",
                table: "Appointments");

            migrationBuilder.DropColumn(
                name: "BillId",
                table: "Appointments");

            migrationBuilder.DropColumn(
                name: "BillId1",
                table: "Appointments");

            migrationBuilder.CreateIndex(
                name: "IX_bills_AppointmentId",
                table: "bills",
                column: "AppointmentId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_bills_Appointments_AppointmentId",
                table: "bills",
                column: "AppointmentId",
                principalTable: "Appointments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_bills_Appointments_AppointmentId",
                table: "bills");

            migrationBuilder.DropIndex(
                name: "IX_bills_AppointmentId",
                table: "bills");

            migrationBuilder.AddColumn<Guid>(
                name: "BillId",
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

            migrationBuilder.CreateIndex(
                name: "IX_Appointments_BillId1",
                table: "Appointments",
                column: "BillId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Appointments_bills_BillId1",
                table: "Appointments",
                column: "BillId1",
                principalTable: "bills",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
