using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HairArt.Migrations
{
    /// <inheritdoc />
    public partial class FixedDeleteBehavior : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Appointments",
                columns: table => new
                {
                    AppointmentId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    EmployeeId = table.Column<int>(type: "int", nullable: false),
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    ScheduleId = table.Column<int>(type: "int", nullable: false),
                    AppointmentDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Appointments", x => x.AppointmentId);
                    table.ForeignKey(
                        name: "FK_Appointments_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Appointments_EmployeeProducts_EmployeeId_ProductId",
                        columns: x => new { x.EmployeeId, x.ProductId },
                        principalTable: "EmployeeProducts",
                        principalColumns: new[] { "EmployeeId", "ProductId" },
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Appointments_EmployeeSchedules_EmployeeId_ScheduleId",
                        columns: x => new { x.EmployeeId, x.ScheduleId },
                        principalTable: "EmployeeSchedules",
                        principalColumns: new[] { "EmployeeId", "ScheduleId" },
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Appointments_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "ProductId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Appointments_EmployeeId_ProductId",
                table: "Appointments",
                columns: new[] { "EmployeeId", "ProductId" });

            migrationBuilder.CreateIndex(
                name: "IX_Appointments_EmployeeId_ScheduleId",
                table: "Appointments",
                columns: new[] { "EmployeeId", "ScheduleId" });

            migrationBuilder.CreateIndex(
                name: "IX_Appointments_ProductId",
                table: "Appointments",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_Appointments_UserId",
                table: "Appointments",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Appointments");
        }
    }
}
