using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GestionBoutiqueBack.Migrations
{
    /// <inheritdoc />
    public partial class AddHireDateToEmployee : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Absences_Employees_EmployeeId",
                table: "Absences");

            migrationBuilder.DropForeignKey(
                name: "FK_PayrollHistories_Employees_EmployeeId",
                table: "PayrollHistories");

            migrationBuilder.DropForeignKey(
                name: "FK_Vacancies_Employees_EmployeeId",
                table: "Vacancies");

            migrationBuilder.DropIndex(
                name: "IX_Vacancies_EmployeeId",
                table: "Vacancies");

            migrationBuilder.DropIndex(
                name: "IX_PayrollHistories_EmployeeId",
                table: "PayrollHistories");

            migrationBuilder.DropIndex(
                name: "IX_Absences_EmployeeId",
                table: "Absences");

            migrationBuilder.DropColumn(
                name: "HireDate",
                table: "Employees");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "HireDate",
                table: "Employees",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.CreateIndex(
                name: "IX_Vacancies_EmployeeId",
                table: "Vacancies",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_PayrollHistories_EmployeeId",
                table: "PayrollHistories",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_Absences_EmployeeId",
                table: "Absences",
                column: "EmployeeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Absences_Employees_EmployeeId",
                table: "Absences",
                column: "EmployeeId",
                principalTable: "Employees",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_PayrollHistories_Employees_EmployeeId",
                table: "PayrollHistories",
                column: "EmployeeId",
                principalTable: "Employees",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Vacancies_Employees_EmployeeId",
                table: "Vacancies",
                column: "EmployeeId",
                principalTable: "Employees",
                principalColumn: "Id");
        }
    }
}
