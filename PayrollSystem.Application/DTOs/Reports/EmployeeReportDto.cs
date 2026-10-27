using System;
using System.Collections.Generic;
using System.Text;

namespace PayrollSystem.Application.DTOs.Reports
{
    public sealed record EmployeeReportDto
    {
        public string Name { get; init; } = string.Empty;
        public string DepartmentName { get; init; } = string.Empty;
        public string JobGradeName { get; init; } = string.Empty;
        public decimal BaseSalary { get; init; }
        public DateTime DateOfBirth { get; init; }
        public DateTime HireDate { get; init; }
        public string Email { get; init; } = string.Empty;
        public string PhoneNumber { get; init; } = string.Empty;
    }
}
