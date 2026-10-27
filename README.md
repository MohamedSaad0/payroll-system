# Payroll & HR Management System

An enterprise-grade HR and Payroll management system built with ASP.NET Core MVC, Entity Framework Core, and strictly adhering to Clean Architecture principles.

## Quick Start (Zero-Click Startup)

This project is built for frictionless onboarding. You do not need to manually run any SQL scripts or EF Core migrations.

1. Clone the repository.
2. Open `PayrollSystem.sln` in Visual Studio or VS Code.
3. Press F5 (or run `dotnet run --project PayrollSystem.Web`).

On startup, the application will automatically create the local SQL Server database (`PayrollSystemDb`), run all migrations, and seed a rich dataset of Departments, Job Grades, Employees, and current-month absences so the Reports dashboard is fully populated and ready for immediate testing.

## Architecture & Design Decisions

This solution is built using Clean Architecture with a strict separation of concerns, divided into 4 projects:

1. **Domain Layer (`PayrollSystem.Domain`)**
   - Implements Rich Domain Models with encapsulation (Private Setters).
   - Domain Guard Clauses enforce business rules directly in memory (e.g., An employee must be 18+ years old at their `HireDate` vs `DateOfBirth`).

2. **Application Layer (`PayrollSystem.Application`)**
   - Defines strictly separated `DTOs` for Form submission vs. Read-Only grids to prevent over-posting vulnerabilities.
   - Enforces cross-property validation using `IValidatableObject` natively in the MVC pipeline.

3. **Infrastructure Layer (`PayrollSystem.Infrastructure`)**
   - EF Core mappings via Fluent API (`IEntityTypeConfiguration`).
   - Entity-Specific Repository Pattern. Note: I explicitly opted out of the generic `IUnitOfWork` pattern here as it is an abstraction leak for simple vertical slices, utilizing EF Core's native DbContext as the Unit of Work.

4. **Web Layer (`PayrollSystem.Web`)**
   - ASP.NET Core MVC with PRG (Post-Redirect-Get) pattern implemented across all POST operations.
   - Fully localized UI using Bootstrap 5 RTL.

## Highlighted Technical Implementations 

I focused heavily on database performance, data integrity, and avoiding complex `if` statements in the business logic:

* **Dynamic Math Engine (Zero `if` statements):** The `ReportService` calculates the Seniority Incentive dynamically by leveraging SQL Server's B-Tree descending index logic and `FirstOrDefault()`, scaling infinitely if HR adds new brackets.
* **Graceful Edge Case Handling:** The "2 days absence" rule (0% incentive, 0% deduction) is handled purely by the structural gap in the seeded DB data, requiring zero hardcoded C# `if (days == 2)` logic.
* **Composite Indexes for Data Integrity:** Implemented a Unique Composite Index on `EmployeeAbsence (EmployeeId, Year, Month)`. The Application Service gracefully catches duplicates using `GetByEmployeeAndMonthAsync` rather than allowing raw `DbUpdateExceptions` to crash the server.
* **N+1 Query Prevention:** Eager loading (`.Include`) and `.AsNoTracking()` are strictly utilized in the repository read operations to eliminate memory bloat on large UI grids.

## Database Schema (ERD)

The database is highly optimized with precise data types and composite indexing strategies.

```mermaid
erDiagram
    Department ||--o{ Employee : "has (1:N)"
    JobGrade ||--o{ Employee : "categorizes (1:N)"
    Employee ||--o{ EmployeeAbsence : "records (1:N)"

    Department {
        int Id PK
        string Name UK "IX_Departments_Name (Unique)"
        decimal IncentivePercentage "حافز القسم (%)"
    }

    JobGrade {
        int Id PK
        string Name UK "IX_JobGrades_Name (أولى - ثانية - ثالثة)"
        decimal BaseSalary "المرتب الأساسي للدرجة"
    }

    Employee {
        int Id PK
        string Name "IX_Employees_Name"
        date DateOfBirth "تاريخ الميلاد"
        string Address "العنوان"
        string PhoneNumber "رقم الهاتف"
        string Email UK "IX_Employees_Email (Unique)"
        date HireDate "تاريخ التعيين"
        int JobGradeId FK "IX_Employees_JobGradeId"
        int DepartmentId FK "IX_Employees_DepartmentId"
    }

    SeniorityIncentiveRule {
        int Id PK
        int MinYears UK "IX_SeniorityRules_MinYears (Unique)"
        decimal IncentivePercentage "نسبة الحافز (%)"
    }

    AbsencePolicyRule {
        int Id PK
        string TierName "وصف الحالة (أكثر من يومين، إلخ)"
        int FromDays "IX_AbsenceRules_FromDays_ToDays"
        int ToDays "الحد الأقصى للأيام (Required)"
        decimal Percentage "النسبة المئوية (%)"
        bool IsIncentive "True = حافز / False = خصم"
    }

    EmployeeAbsence {
        int Id PK
        int EmployeeId FK "IX_EmployeeAbsences_Emp_Year_Month (Unique Composite)"
        int Year "السنة"
        int Month "الشهر"
        int AbsenceDaysCount "عدد أيام الغياب"
    }