using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HairArt.Migrations
{
    /// <inheritdoc />
    public partial class UpdateAppointment : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Appointments_AspNetUsers_UserId",
                table: "Appointments");

            migrationBuilder.DropForeignKey(
                name: "FK_Appointments_Employees_EmployeeId",
                table: "Appointments");

            migrationBuilder.DropForeignKey(
                name: "FK_Appointments_Products_ProductId",
                table: "Appointments");

            migrationBuilder.DropIndex(
                name: "IX_Appointments_ProductId",
                table: "Appointments");

            migrationBuilder.AlterColumn<int>(
                name: "ScheduleId",
                table: "EmployeeSchedules",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .Annotation("Relational:ColumnOrder", 1);

            migrationBuilder.AlterColumn<int>(
                name: "EmployeeId",
                table: "EmployeeSchedules",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .Annotation("Relational:ColumnOrder", 0);

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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

            migrationBuilder.AlterColumn<int>(
                name: "ScheduleId",
                table: "EmployeeSchedules",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .OldAnnotation("Relational:ColumnOrder", 1);

            migrationBuilder.AlterColumn<int>(
                name: "EmployeeId",
                table: "EmployeeSchedules",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .OldAnnotation("Relational:ColumnOrder", 0);

            migrationBuilder.CreateIndex(
                name: "IX_Appointments_ProductId",
                table: "Appointments",
                column: "ProductId");

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

            migrationBuilder.AddForeignKey(
                name: "FK_Appointments_Products_ProductId",
                table: "Appointments",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "ProductId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
