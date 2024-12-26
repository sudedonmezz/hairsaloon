﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HairArt.Migrations
{
    /// <inheritdoc />
    public partial class UpdateRelations : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EmployeeSchedules_Schedules_ScheduleId",
                table: "EmployeeSchedules");

            migrationBuilder.AddForeignKey(
                name: "FK_EmployeeSchedules_Schedules_ScheduleId",
                table: "EmployeeSchedules",
                column: "ScheduleId",
                principalTable: "Schedules",
                principalColumn: "ScheduleId",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EmployeeSchedules_Schedules_ScheduleId",
                table: "EmployeeSchedules");

            migrationBuilder.AddForeignKey(
                name: "FK_EmployeeSchedules_Schedules_ScheduleId",
                table: "EmployeeSchedules",
                column: "ScheduleId",
                principalTable: "Schedules",
                principalColumn: "ScheduleId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
