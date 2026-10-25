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
    }
}