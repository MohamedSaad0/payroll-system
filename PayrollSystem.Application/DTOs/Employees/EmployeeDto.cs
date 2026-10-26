using System;
using System.Collections.Generic;
using System.Text;

namespace PayrollSystem.Application.DTOs.Employees
{
    public sealed record EmployeeDto
    {
        public int Id { get; init; }
        public string Name { get; init; } = string.Empty;
        public string Email { get; init; } = string.Empty;
        public string PhoneNumber { get; init; } = string.Empty;
        public DateTime HireDate { get; init; }
        public string DepartmentName { get; init; } = string.Empty;
        public string JobGradeName { get; init; } = string.Empty;


    }
}
