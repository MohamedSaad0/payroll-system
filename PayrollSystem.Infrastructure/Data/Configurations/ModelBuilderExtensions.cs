using Microsoft.EntityFrameworkCore;
using PayrollSystem.Domain.Entities;

namespace PayrollSystem.Infrastructure.Data;

public static class ModelBuilderExtensions
{
    public static void Seed(this ModelBuilder modelBuilder)
    {

        modelBuilder.Entity<JobGrade>().HasData(
            new { Id = 1, Name = "أولى", BaseSalary = 10000m },
            new { Id = 2, Name = "ثانية", BaseSalary = 7000m },
            new { Id = 3, Name = "ثالثة", BaseSalary = 4000m }
        );


        modelBuilder.Entity<SeniorityIncentive>().HasData(
            new { Id = 1, MinYears = 5, IncentivePercentage = 10.00m },
            new { Id = 2, MinYears = 7, IncentivePercentage = 15.00m }
        );


        modelBuilder.Entity<AbsencePolicy>().HasData(
            new { Id = 1, Bracket = "يوم أو لا يوجد", FromDays = 0, ToDays = 1, Percentage = 5.00m, IsIncentive = true },
            new { Id = 2, Bracket = "أكثر من يومين", FromDays = 3, ToDays = 5, Percentage = 5.00m, IsIncentive = false },
            new { Id = 3, Bracket = "أكثر من 5 أيام", FromDays = 6, ToDays = 10, Percentage = 10.00m, IsIncentive = false },
            new { Id = 4, Bracket = "أكثر من 10 أيام", FromDays = 11, ToDays = 15, Percentage = 15.00m, IsIncentive = false },
            new { Id = 5, Bracket = "أكثر من 15 يوم", FromDays = 16, ToDays = 31, Percentage = 25.00m, IsIncentive = false }
        );


        modelBuilder.Entity<Department>().HasData(
            new { Id = 1, Name = "تكنولوجيا المعلومات (IT)", IncentivePercentage = 15.00m },
            new { Id = 2, Name = "الموارد البشرية (HR)", IncentivePercentage = 10.00m }
        );


        modelBuilder.Entity<Employee>().HasData(
            new { Id = 1, Name = "أحمد محمود كمال", DateOfBirth = new DateTime(1990, 5, 15), Address = "الإسكندرية، مصر", PhoneNumber = "01012345678", Email = "ahmed@company.com", HireDate = new DateTime(2015, 3, 1), JobGradeId = 1, DepartmentId = 1 },
            new { Id = 2, Name = "سارة طارق سعيد", DateOfBirth = new DateTime(1995, 8, 22), Address = "القاهرة، مصر", PhoneNumber = "01198765432", Email = "sara@company.com", HireDate = new DateTime(2019, 6, 10), JobGradeId = 2, DepartmentId = 2 },
            new { Id = 3, Name = "كريم مصطفى فهمي", DateOfBirth = new DateTime(1998, 11, 5), Address = "الجيزة، مصر", PhoneNumber = "01234567890", Email = "karim@company.com", HireDate = new DateTime(2023, 1, 20), JobGradeId = 3, DepartmentId = 1 }
        );


        var currentYear = DateTime.Today.Year;
        var currentMonth = DateTime.Today.Month;

        modelBuilder.Entity<EmployeeAbsence>().HasData(
            new { Id = 1, EmployeeId = 1, Year = currentYear, Month = currentMonth, AbsenceDaysCount = 0 },
            new { Id = 2, EmployeeId = 2, Year = currentYear, Month = currentMonth, AbsenceDaysCount = 2 },
            new { Id = 3, EmployeeId = 3, Year = currentYear, Month = currentMonth, AbsenceDaysCount = 7 }
        );
    }
}