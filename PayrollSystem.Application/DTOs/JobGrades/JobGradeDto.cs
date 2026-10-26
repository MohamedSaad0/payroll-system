using System;
using System.Collections.Generic;
using System.Text;

namespace PayrollSystem.Application.DTOs.JobGrades
{
    public sealed record JobGradeDto
    {
        public int Id { get; init; }
        public string Name { get; init; } = string.Empty;
        public decimal BaseSalary { get; init; }
    }
}
