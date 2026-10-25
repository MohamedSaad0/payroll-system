using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace PayrollSystem.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AbsencePolicies",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Bracket = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    FromDays = table.Column<int>(type: "int", nullable: false),
                    ToDays = table.Column<int>(type: "int", nullable: false),
                    Percentage = table.Column<decimal>(type: "decimal(5,2)", nullable: false),
                    IsIncentive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AbsencePolicies", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Departments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    IncentivePercentage = table.Column<decimal>(type: "decimal(5,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Departments", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "JobGrades",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    BaseSalary = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JobGrades", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SeniorityIncentives",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MinYears = table.Column<int>(type: "int", nullable: false),
                    IncentivePercentage = table.Column<decimal>(type: "decimal(5,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SeniorityIncentives", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Employees",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    DateOfBirth = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Address = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    HireDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    JobGradeId = table.Column<int>(type: "int", nullable: false),
                    DepartmentId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Employees", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Employees_Departments_DepartmentId",
                        column: x => x.DepartmentId,
                        principalTable: "Departments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Employees_JobGrades_JobGradeId",
                        column: x => x.JobGradeId,
                        principalTable: "JobGrades",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "EmployeeAbsences",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Year = table.Column<int>(type: "int", nullable: false),
                    Month = table.Column<int>(type: "int", nullable: false),
                    AbsenceDaysCount = table.Column<int>(type: "int", nullable: false),
                    EmployeeId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmployeeAbsences", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EmployeeAbsences_Employees_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "Employees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "AbsencePolicies",
                columns: new[] { "Id", "Bracket", "FromDays", "IsIncentive", "Percentage", "ToDays" },
                values: new object[,]
                {
                    { 1, "يوم أو لا يوجد", 0, true, 5.00m, 1 },
                    { 2, "أكثر من يومين", 3, false, 5.00m, 5 },
                    { 3, "أكثر من 5 أيام", 6, false, 10.00m, 10 },
                    { 4, "أكثر من 10 أيام", 11, false, 15.00m, 15 },
                    { 5, "أكثر من 15 يوم", 16, false, 25.00m, 31 }
                });

            migrationBuilder.InsertData(
                table: "JobGrades",
                columns: new[] { "Id", "BaseSalary", "Name" },
                values: new object[,]
                {
                    { 1, 10000m, "أولى" },
                    { 2, 7000m, "ثانية" },
                    { 3, 4000m, "ثالثة" }
                });

            migrationBuilder.InsertData(
                table: "SeniorityIncentives",
                columns: new[] { "Id", "IncentivePercentage", "MinYears" },
                values: new object[,]
                {
                    { 1, 10.00m, 5 },
                    { 2, 15.00m, 7 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_AbsencePolicies_FromDays_ToDays",
                table: "AbsencePolicies",
                columns: new[] { "FromDays", "ToDays" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Departments_Name",
                table: "Departments",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeAbsences_EmployeeId_Year_Month",
                table: "EmployeeAbsences",
                columns: new[] { "EmployeeId", "Year", "Month" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeAbsences_Year_Month",
                table: "EmployeeAbsences",
                columns: new[] { "Year", "Month" });

            migrationBuilder.CreateIndex(
                name: "IX_Employees_DepartmentId",
                table: "Employees",
                column: "DepartmentId");

            migrationBuilder.CreateIndex(
                name: "IX_Employees_Email",
                table: "Employees",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Employees_JobGradeId",
                table: "Employees",
                column: "JobGradeId");

            migrationBuilder.CreateIndex(
                name: "IX_Employees_Name",
                table: "Employees",
                column: "Name");

            migrationBuilder.CreateIndex(
                name: "IX_JobGrades_Name",
                table: "JobGrades",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_SeniorityRules_MinYears",
                table: "SeniorityIncentives",
                column: "MinYears",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AbsencePolicies");

            migrationBuilder.DropTable(
                name: "EmployeeAbsences");

            migrationBuilder.DropTable(
                name: "SeniorityIncentives");

            migrationBuilder.DropTable(
                name: "Employees");

            migrationBuilder.DropTable(
                name: "Departments");

            migrationBuilder.DropTable(
                name: "JobGrades");
        }
    }
}
