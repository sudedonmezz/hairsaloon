using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HairArt.Migrations
{
    /// <inheritdoc />
    public partial class DenemeIki : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CategoryId",
                table: "Appointments",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsApproved",
                table: "Appointments",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateIndex(
                name: "IX_Appointments_CategoryId",
                table: "Appointments",
                column: "CategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_Appointments_Categories_CategoryId",
                table: "Appointments",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "CategoryId",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Appointments_Categories_CategoryId",
                table: "Appointments");

            migrationBuilder.DropIndex(
                name: "IX_Appointments_CategoryId",
                table: "Appointments");

            migrationBuilder.DropColumn(
                name: "CategoryId",
                table: "Appointments");

            migrationBuilder.DropColumn(
                name: "IsApproved",
                table: "Appointments");
        }
    }
}
