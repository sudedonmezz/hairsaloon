using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HairArt.Migrations
{
    /// <inheritdoc />
    public partial class UpdateAppointmentEmployeeScheduleRelation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Appointments_AspNetUsers_UserId",
                table: "Appointments");

            migrationBuilder.DropForeignKey(
                name: "FK_Appointments_EmployeeProducts_EmployeeId_ProductId",
                table: "Appointments");

            migrationBuilder.DropIndex(
                name: "IX_Appointments_EmployeeId_ProductId",
                table: "Appointments");

            migrationBuilder.AddForeignKey(
                name: "FK_Appointments_AspNetUsers_UserId",
                table: "Appointments",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Appointments_Employees_EmployeeId",
                table: "Appointments",
                column: "EmployeeId",
                principalTable: "Employees",
                principalColumn: "EmployeeId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Appointments_AspNetUsers_UserId",
                table: "Appointments");

            migrationBuilder.DropForeignKey(
                name: "FK_Appointments_Employees_EmployeeId",
                table: "Appointments");

            migrationBuilder.CreateIndex(
                name: "IX_Appointments_EmployeeId_ProductId",
                table: "Appointments",
                columns: new[] { "EmployeeId", "ProductId" });

            migrationBuilder.AddForeignKey(
                name: "FK_Appointments_AspNetUsers_UserId",
                table: "Appointments",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Appointments_EmployeeProducts_EmployeeId_ProductId",
                table: "Appointments",
                columns: new[] { "EmployeeId", "ProductId" },
                principalTable: "EmployeeProducts",
                principalColumns: new[] { "EmployeeId", "ProductId" },
                onDelete: ReferentialAction.Restrict);
        }
    }
}
