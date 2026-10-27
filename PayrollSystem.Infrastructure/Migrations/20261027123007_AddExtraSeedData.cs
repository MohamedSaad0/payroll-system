using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace PayrollSystem.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddExtraSeedData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Departments",
                columns: new[] { "Id", "IncentivePercentage", "Name" },
                values: new object[,]
                {
                    { 1, 15.00m, "تكنولوجيا المعلومات (IT)" },
                    { 2, 10.00m, "الموارد البشرية (HR)" }
                });

            migrationBuilder.InsertData(
                table: "Employees",
                columns: new[] { "Id", "Address", "DateOfBirth", "DepartmentId", "Email", "HireDate", "JobGradeId", "Name", "PhoneNumber" },
                values: new object[,]
                {
                    { 1, "الإسكندرية، مصر", new DateTime(1990, 5, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, "ahmed@company.com", new DateTime(2015, 3, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, "أحمد محمود كمال", "01012345678" },
                    { 2, "القاهرة، مصر", new DateTime(1995, 8, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, "sara@company.com", new DateTime(2019, 6, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, "سارة طارق سعيد", "01198765432" },
                    { 3, "الجيزة، مصر", new DateTime(1998, 11, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, "karim@company.com", new DateTime(2023, 1, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), 3, "كريم مصطفى فهمي", "01234567890" }
                });

            migrationBuilder.InsertData(
                table: "EmployeeAbsences",
                columns: new[] { "Id", "AbsenceDaysCount", "EmployeeId", "Month", "Year" },
                values: new object[,]
                {
                    { 1, 0, 1, 10, 2026 },
                    { 2, 2, 2, 10, 2026 },
                    { 3, 7, 3, 10, 2026 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "EmployeeAbsences",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "EmployeeAbsences",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "EmployeeAbsences",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Departments",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Departments",
                keyColumn: "Id",
                keyValue: 2);
        }
    }
}
