using System;
using System.Collections.Generic;
using System.Text;

namespace PayrollSystem.Application.DTOs.Departments
{
    public sealed record DepartmentDto
    {
        public int Id { get; init; }
        public string Name { get; init; } = string.Empty;
        public decimal IncentivePercentage { get; init; }
    }
}

